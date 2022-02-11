<template>
  <div class="EventDetails">
    <el-page-header
      @back="goBack"
      content="Event Details"
    />

    <el-divider />

    <el-row :gutter="20">
      <el-col :span="5">
        <el-card>
          <el-descriptions
            :title="event.name"
            direction="vertical"
            border
            :column="2"
          >
            <el-descriptions-item label="Date">
              {{ event.date | dateFilter }}
            </el-descriptions-item>

            <el-descriptions-item label="Number of registered Attendees">
              {{ event.numOfAttendees }}
            </el-descriptions-item>

            <el-descriptions-item label="Venues" :span="2">
              <el-tag
                v-for="(venue, index) in venues"
                :key="index"
                class="EventDetails__venue-tag"
                size="small"
              >
                {{venue.venueName}} ({{venue.priority}})
              </el-tag>
            </el-descriptions-item>

            <el-descriptions-item label="Event URL" :span="2">
              <i class="el-icon-link" />
              {{event.registrationUrl}}
            </el-descriptions-item>
          </el-descriptions>
        </el-card>
      </el-col>
      <el-col :span="5">
        <el-card>
          <div>
            <el-image
              class="EventDetails__qr-image"
              :src="qrCodeImage"
              fit="contain"
            />
          </div>

          <el-button
            type="primary"
            :disabled="!qrCodeImage"
            @click="printQrCode"
          >
            Print QR Code
          </el-button>
        </el-card>

      </el-col>
      <el-col :span="14">
        <el-card>
          <el-table
            stripe
            border
            style="width: 100%"
            :data="attendees"
            v-loading="isLoadingEventAttendees"
            class="EventTable"
          >
            <el-table-column
              prop="Name"
              label="Name"
            >
              <template v-slot="{ row }">
                {{row.lastName}} {{row.firstName}}
              </template>
            </el-table-column>
            <el-table-column
              prop="phoneNumber"
              label="Phone Number"
            />
            <el-table-column
              prop="venue"
              label="Venue"
            />
            <el-table-column
              prop="category"
              label="Category"
            >
              <template v-slot="{ row }">
                <el-tag >
                  {{row.category}}
                </el-tag>
              </template>
            </el-table-column>
            <el-table-column
              prop="seat"
              label="Seat"
            />
            <el-table-column
              prop="accompanyingSeat"
              label="Accompanying Seat"
            />
          </el-table>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import printJs from 'print-js';

import api from '@/utils/api';

export default {
  name: 'EventDetails',
  props: {
    eventId: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      event: {},
      attendees: [],
      isLoadingEvent: false,
      isLoadingEventAttendees: false
    }
  },
  computed: {
    qrCodeImage() {
      if (this.event) {
        return `data:image/png;base64, ${this.event.registrationUrlQrCode}`;
      }

      return '';
    },
    venues() {
      if (this.event) {
        return (this.event.availableSeats || []).reduce((acc, { venueName, priority }) => {
          const found = acc.find(v => v.venueName === venueName);

          if (!found) {
            acc.push({ venueName, priority });
          }

          return acc;
        }, []);
      }

      return [];
    }
  },
  methods: {
    goBack() {
      this.$router.push({ path: '/events' });
    },
    async getEventDetails() {
      this.isLoadingEvent = true;

      try {
        const { data } = await api.get(`/v1/events/${this.eventId}`);

        this.event = data;
      } catch (error) {
        const { data, status } = error.response;

        console.log(status, data.message);
      } finally {
        this.isLoadingEvent = false;
      }
    },
    async getEventAttendees() {
      this.isLoadingEventAttendees = true;

      try {
        const { data } = await api.get(`/v1/events/${this.eventId}/attendees`);

        this.event = data;
      } catch (error) {
        const { data, status } = error.response;

        console.log(status, data.message);
      } finally {
        this.isLoadingEventAttendees = false;
      }
    },
    printQrCode() {
      printJs({
        printable: this.qrCodeImage,
        type: 'image',
        base64: true
      });
    }
  },
  mounted() {
    this.getEventDetails();
    this.getEventAttendees();
  }
}
</script>

<style lang="scss">
  .EventDetails {
    &__qr-image {
      width: 100%;
      border: 1px solid #d7dae2;
    }

    &__venue-tag:not(:first-of-type) {
      margin-left: 4px;
    }
  }
</style>