import Vue from 'vue';
import VueRouter from 'vue-router';

// open routes
const Home = () => import('../views/open/Home.vue');
const AttendeeRegistration = () => import('../views/open/AttendeeRegistration');
const RegisterAttendance = () => import('../views/open/RegisterAttendance');

// admin routes
const AdminHome = () => import('../views/admin/AdminHome.vue');
const Login = () => import('../views/admin/auth/Login.vue');
const Venues = () => import('../views/admin/Venues.vue');
const Events = () => import('../views/admin/Events.vue');
const Admins = () => import('../views/admin/Admins.vue');
const EventDetails = () => import('../views/admin/EventDetails');
const VenueDetails = () => import('../views/admin/VenueDetails');

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
    path: '/events/:eventId',
    name: 'AttendeeRegistration',
    component: AttendeeRegistration,
    props: true
  },
  {
    path: '/attendance/register',
    name: 'RegisterAttendance',
    component: RegisterAttendance
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
        path: 'venues/:venueId',
        name: 'VenueDetails',
        component: VenueDetails,
        props: true
      },
      {
        path: 'admins',
        name: 'Admins',
        component: Admins
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
    !localStorage.getItem('token')
  ) {
    next({ name: 'Login' });
  } else {
    next();
  }
});

export default router;
