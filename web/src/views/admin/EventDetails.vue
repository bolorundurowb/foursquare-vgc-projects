<template>
  <div class="EventDetails">
    <el-page-header
      @back="goBack"
      content="Event Details"
    />

    <el-divider />

    <el-row :gutter="20" class="EventDetails__content">
      <el-col :xs="24" :lg="12" :xl="10">
        <el-row :gutter="20">
          <el-col class="EventDetails__col" :xs="24" :sm="12" :lg="14">
            <el-card shadow="never">
              <el-descriptions
                :title="event.name"
                direction="vertical"
                border
                :column="2"
                v-loading="isLoadingEvent"
              >
                <el-descriptions-item label="Date" :span="2">
                  {{ event.startsAt | dateFilter }}
                </el-descriptions-item>

                <el-descriptions-item label="Number of registered Attendees">
                  {{ event.numOfAttendees }}
                </el-descriptions-item>

                <el-descriptions-item label="Event Duration">
                  {{ event.durationInMinutes }} minutes
                </el-descriptions-item>

                <el-descriptions-item label="Venues" :span="2">
                  <el-tag
                    v-for="(venue, index) in venues"
                    :key="index"
                    class="EventDetails__venue-tag"
                    size="small"
                  >
                    {{venue.name}}
                  </el-tag>
                </el-descriptions-item>

                <el-descriptions-item label="Event URL" :span="2">
                  <el-link
                    :href="event.registrationUrl"
                    target="_blank"
                    type="primary"
                    :underline="false"
                    icon="el-icon-link"
                  >
                    {{event.registrationUrl}}
                  </el-link>
                </el-descriptions-item>
              </el-descriptions>
            </el-card>
          </el-col>
          <el-col class="EventDetails__col" :xs="24" :sm="12" :lg="10">
            <el-card shadow="never">
              <div>
                <el-image
                  class="EventDetails__qr-image"
                  :src="qrCodeImage"
                  fit="contain"
                  v-loading="isLoadingEvent"
                >
                  <div slot="error" class="EventDetails__qr-image-error-slot">
                    <i class="el-icon-picture-outline"></i>
                  </div>
                </el-image>
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
        </el-row>
        <el-row :gutter="20">
          <el-col :xs="24" :lg="12" class="EventDetails__col">
            <el-card shadow="never">
              <div slot="header" class="clearfix">
                <span>Change Seats</span>
              </div>
              <change-seat-form
                :is-loading="isChangeSeatLoading"
                :available-seats-and-venues="availableVenuesAndSeats"
                :has-error="changeSeatHasError"
                @submit="handleChangeSeatFormSubmit"
              />
            </el-card>
          </el-col>
          <el-col :xs="24" :lg="12" class="EventDetails__col">
            <el-card shadow="never">
              <div slot="header" class="clearfix">
                <span>Checkin</span>
              </div>

              <person-checkin
                :is-loading="personCheckinLoading"
                :has-error="personCheckinError"
                @submit="handlePersonCheckinSubmit"
              />
            </el-card>
          </el-col>
        </el-row>
      </el-col>
      <el-col class="EventDetails__col" :xs="24" :lg="12" :xl="14">
        <el-row>
          <el-col class="EventDetails__col" xs="24">
            <el-card shadow="never" class="EventDetails__card-">
              <el-button
                  type="text"
                  @click="handleDownloadAttendees"
                >
                  Download Event Attendees
                </el-button>
            </el-card>
          </el-col>

          <el-col class="EventDetails__col" xs="24">
            <el-card shadow="never">
              <el-table
                style="width: 100%"
                :data="attendees"
                v-loading="isLoadingEventAttendees"
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
                  prop="phone"
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
                >
                  <template v-slot="{ row }">
                    <span v-if="row.accompanyingSeat">
                      {{row.accompanyingSeat}}
                    </span>
                    <span v-else>
                      --
                    </span>
                  </template>
                </el-table-column>
              </el-table>
            </el-card>
          </el-col>
        </el-row>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import printJs from 'print-js';
import api from '@/utils/api';
import { AlertMixin } from '@/mixins';

import ChangeSeatForm from '@/components/ChangeSeatForm.vue';
import PersonCheckin from '@/components/AdminPersonCheckin.vue'

export default {
  name: 'EventDetails',
  mixins: [AlertMixin],
  props: {
    eventId: {
      type: String,
      required: true
    }
  },
  components: {
    PersonCheckin,
    ChangeSeatForm
  },
  data() {
    return {
      event: {},
      attendees: [],
      isLoadingEvent: false,
      changeSeatHasError: false,
      personCheckinLoading: false,
      personCheckinError: false,
      isChangeSeatLoading: false,
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
      if (this.event && this.event.availableSeats) {
        return this.event.availableSeats.map(({ name }) => ({ name }));
      }

      return [];
    },
    availableVenuesAndSeats() {
      if (this.event && this.event.availableSeats) {
        return this.event.availableSeats;
      }

      return [];
    }
  },
  methods: {
    goBack() {
      this.$router.push({ path: '/admin/events' });
    },
    async getEventDetails() {
      this.isLoadingEvent = true;

      try {
        const { data } = await api.get(`/v1/events/${this.eventId}`);

        this.event = data;
      } catch (error) {
        this.handleError(error)
      } finally {
        this.isLoadingEvent = false;
      }
    },
    async getEventAttendees() {
      this.isLoadingEventAttendees = true;

      try {
        const { data } = await api.get(`/v1/events/${this.eventId}/attendees`);

        this.attendees = data;
      } catch (error) {
        this.handleError(error)
      } finally {
        this.isLoadingEventAttendees = false;
      }
    },
    async changeSeat(body) {
      this.isChangeSeatLoading = true;
      this.changeSeatHasError = false;

      try {
        await api.post(`/v1/events/${this.event.id}/change-seats`, body);

        this.getEventDetails();
        this.getEventAttendees();
        this.handleSuccess('Seat changed successfully');
      } catch (error) {
        this.changeSeatHasError = true;
        let message;

        if (error.response) {
          const { data } = error.response;
          message = data.message;
        } else {
          message = error.message;
        }

        this.handleError(message);
      } finally {
        this.isChangeSeatLoading = false;
      }
    },
    async attenndeeCheckin(body) {
      this.personCheckinLoading = true;
      this.personCheckinError = false;

      try {
        await api.post(`/v1/events/${this.eventId}/checkin`, body);

        this.getEventDetails();
        this.getEventAttendees();
        this.handleSuccess('Person Checked in successfully');
      } catch (error) {
        this.personCheckinError = true;
        let message;

        if (error.response) {
          const { data } = error.response;
          message = data.message;
        } else {
          message = error.message;
        }

        this.handleError(message);
      } finally {
        this.personCheckinLoading = false;
      }
    },
    async attendeeReportDownload() {
      this.isLoadingEvent = true;

      try {
        const { data } = await api.get(`/v1/events/${this.eventId}/attendees/report`, { responseType: 'blob' });

        const file = new Blob([data]);

        const url = window.URL.createObjectURL(file);
        const link = document.createElement('a');
        link.href = url;
        link.setAttribute('download', `${this.event.name}_attendees.xlsx`);
        document.body.appendChild(link);
        link.click();
      } catch {
        this.handleError(new Error('there was a problem downloding the file'))
      } finally {
        this.isLoadingEvent = false;
      }
    },
    printQrCode() {
      if (this.qrCodeImage) {
        printJs({
          printable: this.qrCodeImage,
          type: 'image',
          base64: true
        });
      }
    },
    handleChangeSeatFormSubmit(body) {
      this.changeSeat(body);
    },
    handlePersonCheckinSubmit(body) {
      this.attenndeeCheckin(body);
    },
    handleDownloadAttendees() {
      this.attendeeReportDownload();
    }
  },
  mounted() {
    this.getEventDetails();
    this.getEventAttendees();
    // this.attendeeReportDownload();
  }
}
</script>

<style lang="scss">
  .EventDetails {
    &__qr-image {
      width: 100%;
      border: 1px solid #d7dae2;
      min-height: 233px;
    }

    &__qr-image-error-slot {
      display: flex;
      justify-content: center;
      align-items: center;
      width: 100%;
      height: 100%;
      min-height: 360px;
      background: #f5f7fa;
      color: #909399;
      font-size: 30px;
    }

    &__venue-tag:not(:first-of-type) {
      margin-left: 4px;
    }

    &__col {
      margin-bottom: 20px;
    }
  }
</style>