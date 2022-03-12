<template>
  <el-container class="AdminHome">
    <el-header class="AdminHome__header">
      <div class="AdminHome__header-logo-container">
        <img src="./../../assets/logo.svg" alt="logo" class="AdminHome__logo">

        <!-- these guys don't have a mobile responsive menu so I am hiding it -->
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
        </el-menu>
      </div>

      <el-dropdown trigger="click" @command="commandHandlers">
        <span class="AdminHome__dropdown">
          Admin <i class="el-icon-arrow-down el-icon--right"></i>
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item command="logout">Logout</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
    </el-header>

    <el-main class="AdminHome__content">
      <router-view/>
    </el-main>
  </el-container>
</template>

<script>
import Cookies from 'js-cookie';

export default {
  name: 'AdminHome',
  methods: {
    commandHandlers(command) {
      if (command === 'logout') {

        Cookies.remove('token', { path: '' });
        Cookies.remove('admin', { path: '' });
        Cookies.remove('expiresAt', { path: '' });

        this.$router.replace({ path: '/login' });
      }
    }
  }
};
</script>

<style lang="scss" scoped>
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
    height: 40px;
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
}
</style>
