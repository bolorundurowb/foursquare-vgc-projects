<template>
  <div class="page">
    <div class="content">
      <div class="logo">
        <img src="../assets/logo.png"/>
        <h2>Register</h2>
        <h6>Reserve a Seat</h6>
      </div>

      <div class="form">
        <form>
          <fieldset>
            <label for="nameField">Name</label>
            <input type="text" placeholder="CJ Patoilo" id="nameField"/>
            <label for="ageRangeField">Age Range</label>
            <select id="ageRangeField">
              <option value="0-13">0-13</option>
              <option value="14-17">14-17</option>
              <option value="18-23">18-23</option>
              <option value="24+">24+</option>
            </select>

            <label for="commentField">Comment</label>
            <textarea placeholder="Hi CJ …" id="commentField"></textarea>

            <input class="button-primary" type="submit" value="Submit"/>
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
        width: 15rem;
      }
    }
  }

  .form {
    background-color: #fff;
    border-radius: 0.5rem;
    padding: 3rem 3rem 1.5rem;
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
