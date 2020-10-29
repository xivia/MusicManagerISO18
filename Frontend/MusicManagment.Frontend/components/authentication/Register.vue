<template>
  <div>
    <form id="register">
        <div class="field">
            <label class="label">Username</label>
            <div class="control">
                <input class="input" v-bind:class="{ 'is-danger': $v.userName.$invalid }" type="text" v-model="userName" placeholder="username">
            </div>
            <p class="help is-danger" v-if="!$v.password.required">Field is required</p>
        </div>
        <div class="field">
            <label class="label">Email</label>
            <div class="control">
                <input class="input" v-bind:class="{ 'is-danger': $v.email.$invalid }" type="text" v-model="email" placeholder="email">
            </div>
            <p class="help is-danger" v-if="!$v.password.required">Field is required</p>
        </div>
        <div class="field">
            <label class="label">Password</label>
            <div class="control">
                <input class="input" v-bind:class="{ 'is-danger': $v.password.$invalid }" type="text" v-model="password" placeholder="password">
            </div>
            <p class="help is-danger" v-if="!$v.password.required">Field is required</p>
        </div>
    </form>
  </div>
</template>

<script lang='ts'>
import Vue from 'vue';
import { required, minLength, between } from 'vuelidate/lib/validators';

export default Vue.extend({
  name: 'Register',

  data () {
    return {
      userName: '',
      email: '',
      password: ''
    }
  },

  validations: {
      userName: {
          required,
          minLength: minLength(4)
      },
      email: {
          required,
          minLength: minLength(4)
      },
      password: {
          required,
          minLength: minLength(4)
      }
  }
})
</script>