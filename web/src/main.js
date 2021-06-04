import axios from "axios";
import App from "./App.vue";
import router from "./router";
import { createApp } from "vue";
import VueAxios from "vue-axios";
import VueSweetalert2 from "vue-sweetalert2";

import 'sweetalert2/dist/sweetalert2.min.css';

const instance = axios.create({
  baseURL: process.env.VUE_APP_API_URL || "http://localhost:5089/api",
});

createApp(App)
  .use(VueAxios, instance)
  .use(VueSweetalert2)
  .use(router)
  .mount("#app");
