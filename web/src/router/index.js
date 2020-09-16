import Vue from "vue";
import VueRouter from "vue-router";
import RegisterAttendance from "../views/RegisterAttendance.vue";

Vue.use(VueRouter);

const routes = [
  {
    path: "/",
    name: "Register",
    component: RegisterAttendance,
    meta: {
      title: 'Register | Neophyte'
    }
  }
];

const router = new VueRouter({
  routes
});

export default router;
