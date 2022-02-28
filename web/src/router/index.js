import Vue from 'vue';
import VueRouter from 'vue-router';
import Cookies from 'js-cookie';

const Home = () => import('../views/attendees/Home.vue');
const AdminHome = () => import('../views/admin/AdminHome.vue');
const Login = () => import('../views/admin/auth/Login.vue');
const Venues = () => import('../views/admin/Venues.vue');
const Events = () => import('../views/admin/Events.vue');
const EventDetails = () => import('../views/admin/EventDetails');
const AttendeeRegistration = () => import('../views/attendees/AttendeeRegistration');

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/register',
    name: 'Register',
    component: () => {}
  },
  {
    path: '/events/:eventId',
    name: 'AttendeeRegistration',
    component: AttendeeRegistration,
    props: true
  },
  {
    path: '/admin',
    name: 'AdminHome',
    component: AdminHome,
    meta: {
      title: 'AdminHome',
      requiresAuth: true
    },
    children: [
      {
        path: 'events',
        name: 'Events',
        component: Events
      },
      {
        path: 'events/:eventId',
        name: 'EventDetails',
        component: EventDetails,
        props: true
      },
      {
        path: 'venues',
        name: 'Venues',
        component: Venues
      },
      {
        path: '',
        redirect: '/admin/events'
      }
    ]
  },
  {
    path: '**',
    redirect: '/'
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
