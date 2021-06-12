<template>
  <div class="page">
    <div class="header">
      <img src="./assets/logo.png"/>
    </div>
    <div class="container">
      <router-view/>
    </div>
  </div>
</template>

<style scoped>
.page {
  padding: 2rem;
}

.page .header {
  margin-top: 2rem;
  margin-bottom: 3rem;
  text-align: center;
}

.page .header img {
  width: 15rem;
}

.page .container {
  display: flex;
  flex-direction: row;
  justify-content: center;
  align-items: center;
}

@media only screen and (max-device-width: 768px) {
  .page {
    padding: 1rem;
  }

  .page .header img {
    width: 12rem;
  }
}
</style>

<style>
.swal-button {
  padding: 0 3rem !important;
  border: 0.1rem solid #9b4dca !important;
  background-color: #9b4dca !important;
}
</style>
