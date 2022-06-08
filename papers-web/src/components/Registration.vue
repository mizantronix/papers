<template>
  <div class="Registration">
    <h1>{{ msg }}</h1>
    <label for="fname">Phone number:</label><br>
    <input type="text" v-model="phone" name="phoneNumber"><br><br>
    <label for="fname">Password:</label><br>
    <input type="text" v-model="password" name="password"><br><br>
    <label for="fname">First name:</label><br>
    <input type="text" v-model="firstName" name="firstName"><br><br>
    <label for="fname">Last name:</label><br>
    <input type="text" v-model="lastName" name="lastName"><br><br>
    <label for="fname">Login:</label><br>
    <input type="text" v-model="login" name="login"><br><br>
    <input type="submit" v-on:click="test()" value="Registration1">
    <input type="submit" v-on:click="register()" value="click">
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'Registration',
  props: {
    msg: String,
  },
  data() {
    return {
      phone: "",
      password: "",
      firstName: "",
      lastName: "",
      login: ""
    }
  },
  methods: {
    test: function() {
      console.log("hello " + this.phone + " " + this.password)
    },
    register: function() {
      axios
        .post('http://localhost:759/users/register', {
          firstName: this.firstName,
          lastName: this.lastName,
          login: this.login,
          phone: this.phone,
          password: this.password
        }, 
        {})
        .then(function (response) {
          console.log(response)
        }) 
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
