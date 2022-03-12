<template>
   <el-dialog
    class="QrCodePrintDialog"
    title="Print QR Code"
    width="30%"
    :visible="showPrintQrDialog"
    :lock-scroll="false"
    :center="true"
    :destroy-on-close="true"
    @close="handleClose"
  >
    <el-card shadow="never" id="qr-code">
      <div id="qr-code">
        <el-image
          class="QrCodePrintDialog__qr-image"
          :src="qrCodeImage"
          fit="contain"
        >
          <div slot="error" class="QrCodePrintDialog__qr-image-error-slot">
            <i class="el-icon-picture-outline"></i>
          </div>
        </el-image>

        <el-divider />

        <el-link
          :href="event.registrationUrl"
          target="_blank"
          type="primary"
          :underline="false"
          style="word-break: break-all;"
        >
          {{event.registrationUrl}}
        </el-link>

        <el-divider />

        Android users can download a QR Code scanner from here
        <el-link
          href="https://play.google.com/store/apps/details?id=com.gamma.scan"
          target="_blank"
          type="primary"
          :underline="false"
        >
          https://play.google.com/store/apps/details?id=com.gamma.scan
        </el-link>
      </div>

      <el-divider />

      <el-button
        id="dont-print"
        type="primary"
        :disabled="!qrCodeImage"
        @click="printQrCode"
      >
        Print QR Code
      </el-button>
    </el-card>
   </el-dialog>
</template>

<script>
import printJs from 'print-js';

export default {
  name: 'QrCodePrintDialog',
  props: {
    event: {
      type: Object,
      required: true
    },
    showPrintQrDialog: {
      type: Boolean,
      default: false
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
    handleClose() {
      this.$emit('close');
    },
    printQrCode() {
      if (this.qrCodeImage) {
        printJs({
          printable: 'qr-code',
          type: 'html',
          documentTitle: 'QR Code for service registration',
          ignoreElements: ['dont-print'],
          targetStyles: ['*'],
          maxWidth: 615
        });
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.QrCodePrintDialog {
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
}
</style>