<template>
  <el-container class="Home">
    <el-main class="Home__content">
      <el-row>
        <el-col :md="10" :xs="24">
          <el-header class="Home__header">
            <img src="./../../assets/logo.svg" alt="logo" class="Home__logo">
          </el-header>

          <div class="Home__content-container">
            <p class="Home__welcome-message">
              Welcome to Foursquare VGC Venue & Event management System
            </p>

            <el-row>
              <el-button plain type="primary" @click="handleGoToAdmin">
                Admin Login
              </el-button>
            </el-row>
          </div>
        </el-col>
        <el-col :md="14" class="Home__content-right hidden-sm-and-down" />
      </el-row>
    </el-main>
  </el-container>
</template>

<script>
export default {
  name: 'Home',
  methods: {
    handleGoToAdmin() {
      this.$router.push('/login');
    }
  }
};
</script>

<style scoped lang="scss">
.Home {
  height: 100vh;
  background-color: #F7F7F8;

  &__content {
    padding: 0;
  }

  &__content-right {
    background-image: url("../../assets/qr.jpeg");
    min-height: 100vh;
    background-repeat: no-repeat;
    background-size: cover;
    clip-path: polygon(10% 0, 100% 0, 100% 100%, 0% 100%);
  }

  &__header {
    display: flex;
    align-items: center;
  }

  &__logo {
    height: 40px;
  }

  &__content-container {
    width: 450px;
    margin: 350px auto 0;

    @media (min-width: 992px) and (max-width: 1199px) {
      width: 300px;
    }

    @media (max-width: 576px) {
      width: 85%;
    }
  }

  &__welcome-message {
    font-size: 36px;

    @media /* (min-width: 992px) and */ (max-width: 1199px) {
      font-size: 24px;
    }
  }
}
</style>
