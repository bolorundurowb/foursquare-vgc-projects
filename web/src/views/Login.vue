<template>
  <div class="Login">
    <el-row justify="center" align="center" type="flex" class="Login__row">
      <el-col :sm="5" :span="5">
        <el-card class="box-card">
          <el-form ref="loginForm" :model="loginForm" :rules="rules">
            <el-form-item label="Email" prop="email">
              <el-input type="email" v-model="loginForm.email" autocomplete="off" />
            </el-form-item>
            <el-form-item label="Password" prop="password">
              <el-input type="password" v-model="loginForm.password" autocomplete="off" />
            </el-form-item>
            <el-form-item>
              <el-button
                type="primary"
                @click="submitForm"
              >
                Login
              </el-button>
            </el-form-item>
          </el-form>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import api from '@/utils/api';
import Cookies from 'js-cookie';

export default {
  name: 'Login',
  data() {
    return {
      loginForm: {
        email: '',
        password: ''
      },
      rules: {
        email: [
          { required: true },
          { type: 'email' }
        ],
        password: [
          { required: true }
        ]
      }
    };
  },
  methods: {
    submitForm() {
      this.$refs.loginForm.validate((valid) => {
        if (valid) {
          this.login(this.loginForm);
        } else {
          return false;
        }
      })
    },
    async login({ email, password }) {
      try {
        const { data } = await api.post('/v2/auth/login', {
          emailAddress: email,
          password
        });

        const { token, admin, expiresAt } = data

        Cookies.set('token', token);
        Cookies.set('admin', admin);
        Cookies.set('expiresAt', expiresAt);

        this.$router.replace({ path: '/' });
      } catch (err) {
        const { data, status } = err.response;

        console.log(status, data.message);
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.Login {
  &__row {
    // min-height: 100vh;
  }
}
</style>