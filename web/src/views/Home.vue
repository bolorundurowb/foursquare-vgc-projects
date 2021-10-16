<template>
  <div>
    <div class="form-container">
      <div class="form-header">
        Check Your Registration Status
      </div>

      <div class="form-sub-header">
        You can check to see if you have been registered to attend our services. If you haven't you'll be prompted
        allowed to register. This is to avoid duplication.
      </div>

      <div class="form">
        <form @submit.prevent="check">
          <fieldset>
            <label for="phone-number">
              Phone Number <span style="color: red">*</span>
            </label>
            <input
                type="tel"
                placeholder="e.g 08012345678"
                id="phone-number"
                v-model="phoneNumber"/>

            <div style="text-align: center">
              <button
                  class="button"
                  type="submit"
                  v-bind:disabled="isLoading">
                Check
              </button>
            </div>
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
              v-model="newPerson.firstName"/>

          <label for="last-name">Last Name</label>
          <input
              type="text"
              placeholder="e.g Doe"
              id="last-name"
              v-model="newPerson.lastName"/>

          <label for="reg-phone-number">
            Phone Number <span style="color: red">*</span>
          </label>
          <input
              type="tel"
              placeholder="e.g 08012345678"
              id="reg-phone-number"
              v-model="newPerson.phone"/>

          <div style="text-align: center">
            <button
                class="button"
                type="submit"
                v-bind:disabled="isRegistering">
              Register
            </button>
          </div>
        </fieldset>
      </form>
    </VueModal>

    <VueModal v-model="showInfoModal" title="Registration Info">
      <div style="padding: 1.5rem">
        <h3>Your registration details</h3>
        <h6 style="text-align: justify">
          You can save a screenshot of this QR code and present it to check in
          at the church premises
        </h6>

        <img v-bind:src="qrUrl" alt="QR code"/>
      </div>
    </VueModal>
  </div>
</template>

<script>
import VueModal from '@kouts/vue-modal';

export default {
  name: 'Home',
  components: {
    VueModal: VueModal
  },
  data() {
    return {
      isLoading: false,
      phoneNumber: '',
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
      if (!this.phoneNumber || !/0\d{10}$/g.test(this.phoneNumber)) {
        this.$swal({
          title: 'Error',
          text: 'A valid phone number is required.',
          icon: 'error'
        });
        return;
      }

      this.isLoading = true;

      try {
        const response = await this.axios.get(
            `/v1/persons/check?phoneNumber=${this.phoneNumber}`
        );
        this.qrUrl = `data:image/png;base64,${response.data}`;

        // show the modal
        this.showInfoModal = true;
      } catch (err) {
        const error = err.response;

        if (error && error.status === 404) {
          this.newPerson.phone = this.phoneNumber;
          this.showRegisterModal = true;
        } else {
          this.$swal({
            title: 'Error',
            text: 'An error occurred when checking your registration status.',
            icon: 'error'
          });
        }
      } finally {
        this.isLoading = false;
      }
    },
    async register() {
      // avoid rage clicks
      if (this.isRegistering) {
        return;
      }

      this.isRegistering = true;

      // validate input
      if (!this.newPerson.phone || !/0\d{10}$/g.test(this.newPerson.phone)) {
        this.$swal({
          title: 'Error',
          text: 'A valid phone number is required.',
          icon: 'error'
        });
        return;
      }

      try {
        const response = await this.axios.post('/v1/persons', this.newPerson);
        this.qrUrl = `data:image/png;base64,${response.data}`;

        // reset the input
        this.newPerson = {};

        // show modal
        this.showRegisterModal = false;
        this.showInfoModal = true;
      } catch (err) {
        const error = err.response;
        let message;

        if (error && error.data) {
          message = error.data.message;
        } else {
          message = 'An error occurred when checking your registration status.';
        }

        this.$swal({
          title: 'Error',
          text: message,
          icon: 'error'
        });
      } finally {
        this.isLoading = false;
      }
    }
  }
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

.form-container .form-header {
  text-align: center;
  font-weight: bold;
  font-size: xx-large;
  margin-bottom: 1.5rem;
}

.form-container .form-sub-header {
  text-align: center;
  font-size: 2rem;
  margin-bottom: 2rem;
}

.form-container h5 {
  text-align: center;
}

.form {
  padding-left: 2rem;
  padding-right: 2rem;
}

@media only screen and (max-device-width: 768px) {
  .form-container {
    padding: 3rem 2rem;
    width: 100%;
  }
}
</style>
