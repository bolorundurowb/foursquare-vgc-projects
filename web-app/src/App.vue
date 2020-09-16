<template>
  <div id="app">
    <div id="nav">
      <router-link to="/login">LOGIN</router-link>
    </div>

    <router-view />

    <vue-snotify></vue-snotify>
  </div>
</template>

<style lang="scss">
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  background-color: #fafafa;
  height: 100%;
}

#nav {
  padding: 30px;
  display: flex;
  flex-direction: row-reverse;

  a {
    font-weight: bold;
    color: #2c3e50;
    text-decoration: none;

    &.router-link-exact-active {
      color: #42b983;
    }
  }
}
</style>
