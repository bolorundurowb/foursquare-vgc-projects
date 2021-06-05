<template>
  <div>
    <div class="form-container">
      <h2>Check Your Status</h2>
      <h5>
        You can check to see if you have been registered to attend our services.
      </h5>

      <div class="form">
        <form @submit.prevent="check">
          <fieldset>
            <label for="phone-number">Phone Number <span style="color: red">*</span></label>
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

    <VueModal v-model="showRegisterModal" title="Register">
      <form @submit.prevent="register">
        <fieldset>
          <label for="first-name">First Name</label>
          <input
              type="text"
              placeholder="e.g John"
              id="first-name"
              v-model="newPerson.firstName"
          />

          <label for="last-name">Last Name</label>
          <input
              type="text"
              placeholder="e.g Doe"
              id="last-name"
              v-model="newPerson.lastName"
          />

          <label for="reg-phone-number">Phone Number <span style="color: red">*</span></label>
          <input
              type="tel"
              placeholder="e.g 08012345678"
              id="reg-phone-number"
              v-model="newPerson.phoneNumber"
          />

          <button class="button" type="submit" v-bind:disabled="isRegistering">
            Register
          </button>
        </fieldset>
      </form>
    </VueModal>
  </div>
</template>

<script>
import VueModal from '@kouts/vue-modal'

export default {
  name: "Home",
  components: {
    'VueModal': VueModal
  },
  data() {
    return {
      isLoading: false,
      phoneNumber: "",
      showRegisterModal: false,
      isRegistering: false,
      newPerson: {},
      showInfoModal: false,
      qrUrl: ''
    };
  },
  methods: {
    async check() {
      // if the button is rage clicked
      if (this.isLoading) {
        return;
      }

      // validate input
      if (!this.phoneNumber || !(/0\d{10}/g.test(this.phoneNumber))) {
        this.$swal("Error", "A valid phone number is required.", "error");
        return;
      }

      this.isLoading = true;

      try {
        const response = await this.axios.get(
            `/v1/persons/check?phoneNumber=${this.phoneNumber}`
        );
        this.qrUrl = `data:image/png;base64,${response}`;
      } catch (err) {
        const error = err.response;

        if (error && error.status === 404) {
          this.showRegisterModal = true;
        } else {
          const message =
              "An error occurred when checking your registration status.";
          this.$swal("Uh oh!", message, "error");
        }
      } finally {
        this.isLoading = false;
      }
    },
    async register() {
      this.isRegistering = true;
    }
  },
};
</script>

<style scoped>
.form-container {
  padding: 5rem 4rem;
  border-radius: 1rem;
  box-shadow: 0 0 2rem rgba(0, 0, 0, 0.1);
  width: 75%;
  margin-left: auto;
  margin-right: auto;
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
