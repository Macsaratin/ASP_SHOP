import { createRouter, createWebHistory } from 'vue-router';
import Home from '../pages/frontend/home.vue';
import Login from '../pages/Login.vue';
import Register from '../pages/Register.vue';
import ProductDetail from '../pages/frontend/ProductDetail.vue';
import Cinemas from '../pages/frontend/Cinemas.vue';
import Product from '../pages/frontend/Product.vue';
import CinemaDetail from '../pages/frontend/CinemaDetail.vue';
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
    component: Register
  },
  {
    path :'/products',
    name: 'Products',
    component: Product,
  },

  {
    path: '/movies/:id',
    name: 'MovieDetail',
    component: ProductDetail,
    props: true 
  },
  {
    path: '/cinemas/:id',
    name: 'CinemaDetail',
    component: CinemaDetail,
    props: true 
  },
  {
    path: '/cinemas',
    name: 'Cinemas',
    component: Cinemas,
  }

];

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior() {
    return { top: 0 };
  }
});

export default router;