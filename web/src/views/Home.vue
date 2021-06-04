<template>
  <div class="form-container">
    <h2>Check Your Status</h2>
    <h5>
      You can check to see if you have been registered to attend our services.
    </h5>

    <form>
      <fieldset>
        <label for="phone-number">Phone Number</label>
        <input type="tel" placeholder="e.g 08012345678" id="phone-number">
        <button class="button" type="button">Check</button>
      </fieldset>
    </form>
  </div>
</template>

<script>
export default {
  name: "Home"
};
</script>

<style scoped>
.form-container {
  padding: 3rem;
  border-radius: 1rem;
  box-shadow: 0 0 1rem rgba(0, 0, 0, 0.1);
  width: 50%;
}

.form-container h2 {
  text-align: center;
}

.form-container h5 {
  text-align: center;
}
</style>
