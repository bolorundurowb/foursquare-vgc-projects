<template>
  <el-form
    :model="newPersonForm"
    ref="newPersonForm"
    label-position="top"
    :rules="newPersonFormRules"
    class="NewPersonForm"
  >
    <el-form-item label="First Name" prop="firstName">
      <el-input
        v-model="newPersonForm.firstName"
        prefix-icon="el-icon-user"
      />
    </el-form-item>

    <el-form-item label="Last Name" prop="lastName">
      <el-input
        v-model="newPersonForm.lastName"
        prefix-icon="el-icon-user"
      />
    </el-form-item>

    <el-form-item label="Phone Number" prop="phone">
      <el-input
        v-model="newPersonForm.phone"
        prefix-icon="el-icon-phone-outline"
        placeholder="08012345678"
      />
    </el-form-item>

    <el-form-item>
      <el-button
        type="primary"
        @click="handleSubmit"
        class="NewPersonForm__submit-btn"
      >
        Add Person
      </el-button>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  name: 'NewPersonForm',
  props: {},
  data() {
    return {
      newPersonForm: {
        firstName: '',
        lastName: '',
        phone: ''
      },
      newPersonFormRules: {
        firstName: [{ required: true, message: 'First name is required', trigger: 'blur' }],
        lastName: [{ required: true, message: 'Last name is required', trigger: 'blur' }],
        phone: [
          { validator: this.validatePhoneNumber, trigger: 'blur' },
          { required: true, message: 'Phone number is required', trigger: 'blur' }
        ]
      }
    };
  },
  methods: {
    handleSubmit() {
      this.$refs.newPersonForm.validate((valid) => {
        if (valid) {
          this.$emit('submit', this.newPersonForm);
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
.NewPersonForm {
  &__submit-btn {
    width: 100%;
  }
}
</style>