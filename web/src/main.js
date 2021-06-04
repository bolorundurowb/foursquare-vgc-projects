import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import VueAxios from "vue-axios";
import axios from "axios";

const app = createApp(App);

const instance = axios.create({
  baseURL: process.env.VUE_APP_API_URL || "http://localhost:5089/api"
});
app.use(VueAxios, instance);

app.use(router);
app.mount("#app");
