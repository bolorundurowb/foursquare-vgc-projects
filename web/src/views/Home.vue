<template>
  <div class="form-container">
    <h2>Check Your Status</h2>
    <h5>
      You can check to see if you have been registered to attend our services.
    </h5>

    <div class="form">
      <form @submit.prevent="check">
        <fieldset>
          <label for="phone-number">Phone Number</label>
          <input
            type="tel"
            placeholder="e.g 08012345678"
            id="phone-number"
            v-model="phoneNumber"
          />
          <button class="button" type="submit" v-bind:disabled="isLoading">
            Check
          </button>
        </fieldset>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "Home",
  data() {
    return {
      isLoading: false,
      phoneNumber: "",
    };
  },
  methods: {
    async check() {
      this.isLoading = true;

      try {
        const response = await this.axios.get(
          `/v1/persons/check?phoneNumber=${this.phoneNumber}`
        );
        console.log(response);
      } catch (err) {
        const error = err.response;

        if (error.status === 404) {
          // TODO: redirect to the registration page
        } else {
          const message =
            "An error occurred when checking your registration status.";
          this.$swal("Uh oh!", message, "error");
        }
      } finally {
        this.isLoading = false;
      }
    },
  },
};
</script>

<style scoped>
.form-container {
  padding: 5rem 4rem;
  border-radius: 1rem;
  box-shadow: 0 0 2rem rgba(0, 0, 0, 0.1);
  width: 50%;
}

.form-container h2 {
  text-align: center;
}

.form-container h5 {
  text-align: center;
}

.form {
  padding-left: 2rem;
  padding-right: 2rem;
}
</style>
