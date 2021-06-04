<template>
  <div class="page">
    <div class="header">
      <img src="./assets/logo.png" />
    </div>
    <div class="container">
      <router-view />
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
</style>
