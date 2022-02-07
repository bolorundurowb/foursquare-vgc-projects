import Vue from 'vue';
import VueRouter from 'vue-router';
import Cookies from 'js-cookie';

const AdminHome = () => import('../views/AdminHome.vue');
const Login = () => import('../views/Login.vue');
const Venues = () => import('../views/Venues.vue');
const Events = () => import('../views/Events.vue');

Vue.use(VueRouter);

const routes = [
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    nme: 'Register',
    component: () => {}
  },
  {
    path: '/',
    name: 'AdminHome',
    component: AdminHome,
    meta: {
      title: 'AdminHome',
      requiresAuth: true
    },
    children: [
      {
        path: '',
        name: 'Venues',
        component: Venues
      },
      {
        path: 'events',
        name: 'Events',
        component: Events
      }
    ]
  }
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});

router.beforeEach((to, from, next) => {
  if (
    to.matched.some((record) => record.meta.requiresAuth) &&
    !Cookies.get('token')
  ) {
    next({ name: 'Login' });
  } else {
    next();
  }
});

export default router;
