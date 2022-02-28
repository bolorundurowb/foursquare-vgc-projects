import Vue from 'vue';
import VueRouter from 'vue-router';
import Cookies from 'js-cookie';

const AdminHome = () => import('../views/AdminHome.vue');
const Login = () => import('../views/Login.vue');
const Venues = () => import('../views/Venues.vue');
const Events = () => import('../views/Events.vue');
const EventDetails = () => import('../views/EventDetails');
const AttendeeRegistration = () => import('../views/attendees/AttendeeRegistration');

Vue.use(VueRouter);

const routes = [
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
