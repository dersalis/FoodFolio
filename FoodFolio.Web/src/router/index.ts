// Composables
import { createRouter, createWebHistory } from 'vue-router'

const routes = [
  {
    path: '',
    redirect: '/daily'
  },
  {
    path: '/daily',
    name: 'Daily',
    component: () => import(/* webpackChunkName: "home" */ '@/views/dashboards/Daily.vue'),
  },
  {
    path: '/weekly',
    name: 'Weekly',
    component: () => import(/* webpackChunkName: "home" */ '@/views/dashboards/Weekly.vue'),
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
})

export default router
