<template>
  <el-container class="AdminHome">
    <el-header class="AdminHome__header">
      <div class="AdminHome__logo" />

      <el-menu
        class="AdminHome__menu"
        mode="horizontal"
        :router="true"
      >
        <el-menu-item
          class="AdminHome__menu-item"
          :index="'/'"
        >
          <i class="el-icon-house"/>
          Venues
        </el-menu-item>
        <el-menu-item
          class="AdminHome__menu-item"
          :index="'/events'"
        >
          <i class="el-icon-date"/>
          Events
        </el-menu-item>
      </el-menu>

      <el-dropdown trigger="click">
        <span class="AdminHome__dropdown">
          Admin <i class="el-icon-arrow-down el-icon--right"></i>
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item>Logout</el-dropdown-item>
          <!-- <el-dropdown-item icon="el-icon-plus">Logout</el-dropdown-item> -->
        </el-dropdown-menu>
      </el-dropdown>
    </el-header>
    <el-main class="AdminHome__content">
      <router-view />
    </el-main>
    
  </el-container>
</template>

<script>
export default {
  name: 'AdminHome'
}
</script>

<style lang="scss" scoped>
.AdminHome {
  height: 100vh;

  &__header {
    box-shadow: 0px 2px 4px rgba(0, 0, 0, 0.18);
    display: flex;
    align-items: center;
  }

  &__content {
    background-color: #F7F7F8;
  }

  &__logo {
    height: 40px;
    width: 40px;
    background-color: #C4C4C4;
    border-radius: 20px;
  }

  &__menu {
    margin-left: 50px;
    border-bottom: none;
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