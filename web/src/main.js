import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import axios from "axios";
import VueAxios from "vue-axios";
import VueSwal from "vue-swal";

Vue.config.productionTip = false;

Vue.use(VueSwal);

const instance = axios.create({
  baseURL: process.env.APP_VUE_API_URL || "http://localhost:5089/v1/"
});
Vue.use(VueAxios, instance);

new Vue({
  router,
  render: function(h) {
    return h(App);
  }
}).$mount("#app");
