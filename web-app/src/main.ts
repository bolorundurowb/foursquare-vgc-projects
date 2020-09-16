import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import axios from 'axios';
import VueAxios from "vue-axios";
import VueSnotify, {SnotifyPosition} from "vue-snotify";
import "vue-snotify/styles/material.css";

Vue.config.productionTip = false;
Vue.use(VueSnotify, {
  toast: {
    position: SnotifyPosition.rightTop,
    pauseOnHover: true,
    showProgressBar: true,
    timeout: 4000
  }
});

const instance = axios.create({
  baseURL: 'http://localhost:5089/v1/'
});
Vue.use(VueAxios, instance);

new Vue({
  router,
  render: h => h(App)
}).$mount("#app");
