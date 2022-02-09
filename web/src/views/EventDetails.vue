<template>
  <div class="EventDetails">
    <el-page-header
      @back="goBack"
      content="Event Details"
    />

    <el-row :gutter="20">
      <el-col :span="12">
        <el-card>
          <el-descriptions>

          </el-descriptions>
        </el-card>
      </el-col>
      <el-col :span="12">
        <el-card>
          <div>
            <el-image
              class="EventDetails__qr-image"
              :src="qrCodeImage"
              fit="contain"
            />
          </div>

          <el-button
            plain
            type="primary"
            :disabled="!qrCodeImage"
            @click="printQrCode"
          >
            Print QR Code
          </el-button>
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
      event: null,
      isLoadingEvent: false
    }
  },
  computed: {
    qrCodeImage() {
      if (this.event) {
        return `data:image/png;base64, ${this.event.registrationUrlQrCode}`;
      }

      return '';
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
  }
}
</script>

<style lang="scss">
  .EventDetails {
    &__qr-image {
      width: 300px;
      height: 300px;
      border: 1px solid #d7dae2;
    }
  }
</style>