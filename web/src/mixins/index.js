export const AlertMixin = {
  methods: {
    handleError(error) {
      const { data = {} } = error.response || {};
      const message = data.message || 'There was an error';

      this.$message({
        message,
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
