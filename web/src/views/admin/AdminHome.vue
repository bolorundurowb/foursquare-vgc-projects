<template>
  <el-container class="AdminHome">
    <el-header class="AdminHome__header">
      <div class="AdminHome__header-logo-container">
        <img src="./../../assets/logo.svg" alt="logo" class="AdminHome__logo">

        <el-menu
            class="AdminHome__menu hidden-sm-and-down"
            mode="horizontal"
            :router="true">
          <el-menu-item
              class="AdminHome__menu-item"
              :index="'/admin/events'">
            <i class="el-icon-date"/>
            Events
          </el-menu-item>
          <el-menu-item
              class="AdminHome__menu-item"
              :index="'/admin/venues'">
            <i class="el-icon-house"/>
            Venues
          </el-menu-item>
          <el-menu-item
              class="AdminHome__menu-item"
              :index="'/admin/admins'">
            <i class="el-icon-user"/>
            Admins
          </el-menu-item>
        </el-menu>
      </div>

      <el-dropdown trigger="click" @command="commandHandlers" class="hidden-sm-and-down">
        <span class="AdminHome__dropdown">
          Admin <i class="el-icon-arrow-down el-icon--right"></i>
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item command="logout">Logout</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>

      <div class="AdminHome__mobile-menu hidden-md-and-up">
        <el-button plain icon="el-icon-more" @click="handleOpenMobileDrawer"></el-button>
      </div>
    </el-header>

    <el-main class="AdminHome__content">
      <router-view/>
    </el-main>

    <el-drawer
      :visible.sync="isMobileDrawerOpen"
      direction="ltr"
      class="Admin__mobile-drawer"
      ref="mobileDrawer"
    >
      <template v-slot:title>
        <div class="AdminHome__header-logo-container">
          <img src="./../../assets/logo.svg" alt="logo" class="AdminHome__logo">
        </div>
      </template>

      <el-menu
        class="AdminHome__mobile-menu"
        :router="true">
        <el-menu-item
          @click="$refs.mobileDrawer.closeDrawer()"
          class="AdminHome__mobile-menu-item"
          :index="'/admin/events'">
          <i class="el-icon-date"/>
          Events
        </el-menu-item>
        <el-menu-item
          @click="$refs.mobileDrawer.closeDrawer()"
          class="AdminHome__mobile-menu-item"
          :index="'/admin/venues'">
          <i class="el-icon-house"/>
          Venues
        </el-menu-item>
        <el-menu-item
          @click="$refs.mobileDrawer.closeDrawer()"
          class="AdminHome__mobile-menu-item"
          :index="'/admin/admins'">
        <i class="el-icon-user"/>
        Admins
      </el-menu-item>
    </el-menu>
    </el-drawer>
  </el-container>
</template>

<script>
export default {
  name: 'AdminHome',
  data() {
    return {
      isMobileDrawerOpen: false
    };
  },
  methods: {
    handleOpenMobileDrawer() {
      this.isMobileDrawerOpen = true;
    },
    commandHandlers(command) {
      if (command === 'logout') {

        localStorage.removeItem('token');
        localStorage.removeItem('admin');
        localStorage.removeItem('expiresAt');

        this.$router.replace({ path: '/login' });
      }
    },
    mobileCommandHandlers(command) {
      switch (command) {
        case 'logout':
          localStorage.removeItem('token');
          localStorage.removeItem('admin');
          localStorage.removeItem('expiresAt');

          this.$router.replace({ path: '/login' });
          break;
        case 'goToEvents':
          this.$router.replace({ path: '/admin/events' });
          break;
        case 'goToVenues':
          this.$router.replace({ path: '/admin/venues' });
          break;
        case 'goToAdmins':
          this.$router.replace({ path: '/admin/admins' });
          break;
      }
    }
  }
};
</script>

<style lang="scss">
.AdminHome {
  height: 100vh;

  &__header {
    box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.18);
    display: flex;
    align-items: center;
    justify-content: space-between;
  }

  &__header-logo-container {
    display: flex;
    align-items: center;
  }

  &__content {
    background-color: #F7F7F8;
  }

  &__logo {
    height: 35px;
  }

  &__menu {
    margin-left: 50px;
    border-bottom: none;
    flex: 1;
  }

  &__menu-item {
    border-bottom: none;
    display: flex;
    align-items: center;

    &:not(:first-of-type) {
      margin-left: 10px;
    }
  }

  .el-drawer {
    width: 65% !important;
  }
}
</style>
