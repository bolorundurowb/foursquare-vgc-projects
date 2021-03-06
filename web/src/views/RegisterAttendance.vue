<template>
  <div class="page">
    <div class="content">
      <div class="logo">
        <img src="../assets/logo.png" />
        <h3>Register</h3>
        <h6>Reserve a seat for next sunday</h6>
      </div>

      <div class="form">
        <form @submit.prevent="register">
          <fieldset>
            <div class="question-line">
              <input
                type="checkbox"
                v-model="attendance.returnedInLastTenDays"
              />
              <label class="label-inline">
                Did you return from a trip overseas in the last 10 days?
              </label>
            </div>

            <div class="question-line">
              <input
                type="checkbox"
                v-model="attendance.liveWithCovidCaregivers"
              />
              <label class="label-inline">
                Do you live with COVID-19 caregivers?
              </label>
            </div>

            <div class="question-line">
              <input type="checkbox" v-model="attendance.caredForSickPerson" />
              <label class="label-inline">
                Do you recently care for any sick individual at home or in a
                hospital?
              </label>
            </div>

            <div>
              Do you presently have a cold, fever, sore throat, loss of smell or
              loss of appetite?
            </div>
            <div class="question-line">
              <input
                type="radio"
                name="has-symptoms"
                value="Yes"
                v-model="attendance.haveCovidSymptoms"
              />
              <label class="label-inline">Yes</label>

              <input
                class="radio"
                type="radio"
                name="has-symptoms"
                value="No"
                v-model="attendance.haveCovidSymptoms"
              />
              <label class="label-inline">No</label>

              <input
                class="radio"
                type="radio"
                name="has-symptoms"
                value="Maybe"
                v-model="attendance.haveCovidSymptoms"
              />
              <label class="label-inline">Maybe</label>
            </div>

            <template v-if="isAllowedToRegister">
              <label>Full Name:</label>
              <input type="text" v-model="attendance.fullName" />

              <label>Email Address:</label>
              <input type="email" v-model="attendance.emailAddress" />

              <div class="question-line row">
                <div class="column">
                  <label>Age (in years):</label>
                  <input type="number" min="17" v-model="attendance.age" />
                </div>
                <div class="column">
                  <label>Gender:</label>
                  <select v-model="attendance.gender">
                    <option disabled value="">Select One</option>
                    <option value="Other">Other</option>
                    <option value="Male">Male</option>
                    <option value="Female">Female</option>
                  </select>
                </div>
              </div>

              <label>Phone Number:</label>
              <input type="tel" v-model="attendance.phone" />

              <label>Residential Address:</label>
              <textarea
                rows="3"
                v-model="attendance.residentialAddress"
              ></textarea>
            </template>

            <button class="button-primary" v-bind:disabled="isLoading">
              Submit
            </button>
          </fieldset>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RegisterAttendance",
  data: function() {
    return {
      isLoading: false,
      attendance: this.getVoidAttendance()
    };
  },
  computed: {
    isAllowedToRegister: function() {
      return (
        this.attendance.caredForSickPerson === false &&
        this.attendance.returnedInLastTenDays === false &&
        this.attendance.liveWithCovidCaregivers === false &&
        this.attendance.haveCovidSymptoms === "No"
      );
    }
  },
  methods: {
    register: async function() {
      if (!this.isAllowedToRegister) {
        this.$swal(
          "Uh oh!",
          "Sorry, you cannot reserve a seat for service at this time.",
          "error"
        );
      } else {
        try {
          this.isLoading = true;
          // change age to number
          this.attendance.age = +this.attendance.age;
          await this.axios.post("/attendance", this.attendance);
          this.attendance = this.getVoidAttendance();
          this.$swal(
            "Success!",
            "You have been successfully registered.",
            "success"
          );
        } catch (err) {
          const error = err.response;
          let message;

          if (error.data.message) {
            message = error.data.message;
          } else if (error.data.messages) {
            message = error.data.messages.join("\r\n");
          } else {
            message = "An error occurred";
          }

          this.$swal("Uh oh!", message, "error");
        } finally {
          this.isLoading = false;
        }
      }
    },
    getVoidAttendance: function() {
      return {
        returnedInLastTenDays: false,
        liveWithCovidCaregivers: false,
        caredForSickPerson: false,
        haveCovidSymptoms: "Maybe",
        emailAddress: "",
        fullName: "",
        age: 18,
        gender: "Other",
        phone: "",
        residentialAddress: ""
      };
    }
  }
};
</script>

<style scoped lang="scss">
.page {
  height: calc(100vh - 9rem);
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;

  .content {
    width: 45%;

    .logo {
      text-align: center;

      img {
        width: 10rem;
      }

      h6 {
        margin-bottom: 1.2rem;
      }
    }
  }

  .form {
    background-color: #fff;
    border-radius: 0.5rem;
    padding: 3rem 3rem 1.5rem;

    .question-line {
      margin-top: 0.5rem;
      margin-bottom: 0.5rem;
    }

    .radio {
      margin-left: 1rem;
    }

    label {
      display: inline !important;
    }
  }
}

@media only screen and (max-device-width: 768px) {
  .page {
    .content {
      width: 90%;
    }

    .form {
      padding: 1rem;
    }
  }
}
</style>
