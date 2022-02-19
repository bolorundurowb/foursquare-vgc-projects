export const AlertMixin = {
  methods: {
    handleError(errorMessage) {
      this.$message({
        message: errorMessage,
        showClose: true,
        type: 'error'
      });
    },
    handleSuccess(message) {
      this.$message({
        message,
        showClose: true,
        type: 'success'
      });
    }
  }
};
