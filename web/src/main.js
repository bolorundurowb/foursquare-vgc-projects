import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import axios from "axios";
import VueAxios from "vue-axios";
import VueSwal from "vue-swal";

import "sweetalert2/dist/sweetalert2.min.css";
import "@sweetalert2/theme-bulma/bulma.css";
import "@kouts/vue-modal/dist/vue-modal.css";

Vue.config.productionTip = false;

Vue.use(VueSwal);

const instance = axios.create({
  baseURL: process.env.VUE_APP_API_URL || "http://localhost:5089/v1/"
});
Vue.use(VueAxios, instance);

new Vue({
  router,
  render: (h) => h(App),
}).$mount("#app");
