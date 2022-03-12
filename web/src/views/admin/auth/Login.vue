<template>
  <div class="Login">
    <el-row justify="center" align="middle" type="flex" class="Login__row">
      <el-col :xs="16" :sm="10" :md="6"  :span="5">
        <el-card class="box-card" shadow="never">
          <img src="./../../../assets/logo.svg" alt="logo" class="Login__logo">
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
                v-loading="isLoading"
                :disabled="isLoading"
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
import { AlertMixin } from '@/mixins';

export default {
  name: 'Login',
  mixins: [AlertMixin],
  data() {
    return {
      isLoading: false,
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
      this.isLoading = true;
      try {
        const { data } = await api.post('/v2/auth/login', {
          emailAddress: email,
          password
        });

        const { token, admin, expiresAt } = data

        localStorage.setItem('token', token);
        localStorage.setItem('admin', JSON.stringify(admin));
        localStorage.setItem('expiresAt', expiresAt);

        this.$router.replace({ path: '/admin/events' });
      } catch (error) {
        this.handleError(error);
      } finally {
        this.isLoading = false;
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.Login {
  min-height: 100vh;

  &__row {
    min-height: 100vh;
  }

  &__logo {
    height: 40px;
    margin-bottom: 30px;
  }
}
</style>
