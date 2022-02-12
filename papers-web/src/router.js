import { createWebHistory, createRouter } from "vue-router";

//import auth from './auth'
import App from './App.vue'
import Login from './components/Login.vue'
import About from './components/About.vue'
import Registration from './components/Registration.vue'
import SimpleSettings from './components/SimpleSettings.vue'

const routes = [
  {
    path: "/",
    name: "App",
    component: App,
  },
  {
    path: "/login",
    name: "Login",
    component: Login,
  },
  {
    path: "/about",
    name: "About",
    component: About,
  },
  {
    path: "/registration",
    name: "Registration",
    component: Registration,
  },
  {
    path: "/settings",
    name: "SimpleSettings",
    component: SimpleSettings,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;






/*
function requireAuth (to, from, next) {
    if (!auth.loggedIn()) {
      next({
        path: '/login',
        query: { redirect: to.fullPath }
      })
    } else {
      next()
    }
  }

const router = new VueRouter({
    mode: 'history',
    base: __dirname,
    routes: [
      { path: '/about', component: About },
      //{ path: '/bla', component: Bla, beforeEnter: requireAuth },
      { path: '/login', component: Login },
      { path: '/registration', component: Registration },
      { path: '/settings', component: SimpleSettings },
    ]
  })*/