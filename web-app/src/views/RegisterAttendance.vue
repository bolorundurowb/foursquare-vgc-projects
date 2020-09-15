<template>
  <div class="page">
    <div class="content">
      <div class="logo">
        <img src="../assets/logo.png"/>
        <h3>Register</h3>
        <h6>Reserve a seat for next sunday</h6>
      </div>

      <div class="form">
        <form>
          <fieldset>
            <div class="question-line">
              <input type="checkbox">
              <label class="label-inline">Did you return from a trip overseas in the last 10 days?</label>
            </div>

            <div class="question-line">
              <input type="checkbox">
              <label class="label-inline">Do you live with COVID-19 caregivers?</label>
            </div>

            <div class="question-line">
              <input type="checkbox">
              <label class="label-inline">Do you recently care for any sick individual at home or in a hospital?</label>
            </div>

            <div>Do you presently have a cold, fever, sore throat, loss of smell or loss of appetite?</div>
            <div class="question-line">
              <input type="radio" name="has-symptoms">
              <label class="label-inline">Yes</label>
              <input class="radio" type="radio" name="has-symptoms">
              <label class="label-inline">No</label>
              <input class="radio" type="radio" name="has-symptoms">
              <label class="label-inline">Maybe</label>
            </div>

            <label>Full Name:</label>
            <input type="text"/>

            <label>Email Address:</label>
            <input type="email"/>

            <div class="question-line row">
              <div class="column">
                <label>Age (in years):</label>
                <input type="number"/>
              </div>
              <div class="column">
                <label>Gender:</label>
                <select>
                  <option value="Other">Other</option>
                  <option value="Male">Male</option>
                  <option value="Female">Female</option>
                </select>
              </div>
            </div>

            <label>Phone Number:</label>
            <input type="tel"/>

            <label>Residential Address:</label>
            <textarea
                rows="3"></textarea>

            <input class="button-primary"
                   type="submit"
                   value="Submit"/>
          </fieldset>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "RegisterAttendance"
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
    width: 35%;

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
