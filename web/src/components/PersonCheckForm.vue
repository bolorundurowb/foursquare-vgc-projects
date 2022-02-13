<template>
  <el-form
    :model="personCheckForm"
    ref="personCheckForm"
    label-position="top"
    :rules="personCheckFormRules"
    class="PersonCheckForm"
  >

    <el-form-item label="Phone Number" prop="phoneNumber">
      <el-input
        v-model="personCheckForm.phoneNumber"
        prefix-icon="el-icon-phone-outline"
        placeholder="08012345678"
      />
    </el-form-item>

    <el-form-item>
      <el-button
        type="primary"
        @click="handleSubmit"
        class="PersonCheckForm__submit-btn"
      >
        Get Info
      </el-button>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  name: 'PersonCheckForm',
  props: {},
  data() {
    return {
      personCheckForm: {
        firstName: '',
        lastName: '',
        phone: ''
      },
      personCheckFormRules: {
        phoneNumber: [
          { validator: this.validatePhoneNumber, trigger: 'blur' },
          { required: true, message: 'Phone number is required', trigger: 'blur' }
        ]
      }
    };
  },
  methods: {
    handleSubmit() {
      this.$refs.personCheckForm.validate((valid) => {
        if (valid) {
          this.$emit('submit', this.personCheckForm);
        } else {
          return false;
        }
      });
    },
    validatePhoneNumber(rule, value, callback) {
      if (value === '') {
        callback(new Error('Phone Number is required'));
      } else if (!(/^0[8|9|7][0|1]\d{8}$/g.test(value))) {
        callback(new Error('Please enter a valid phone number'));
      } else {
        callback();
      }
    }
  }
}
</script>

<style lang="scss">
.PersonCheckForm {
  &__submit-btn {
    width: 100%;
  }
}
</style>