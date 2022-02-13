<template>
  <p v-if="isAuth()">Переключение на окно чата</p>
  <div v-else class="wrapper">
    <Login msg="test"/>
    <Registration msg="test2"/>
  </div>
</template>

<script>
//import MainPageSelector from './components/MainPageSelector.vue'
import Login from './components/Login.vue'
import Registration from './components/Registration.vue'


export default {
  name: 'App',
  components: {
    Login,
    Registration,
    // MainPageSelector
  },
  methods:{
    isAuth() {
      return false
    },
  }
}
</script>

<style>
@import './styles/common.css'
</style>
