<template>
  <el-form
    :model="newAttendeeForm"
    ref="newPersonForm"
    label-position="top"
    :rules="newAttendeeFormRules"
    class="NewAttendeeForm"
  >
    <el-form-item label="First Name" prop="firstName">
      <el-input
        v-model="newAttendeeForm.firstName"
        prefix-icon="el-icon-user"
      />
    </el-form-item>

    <el-form-item label="Last Name" prop="lastName">
      <el-input
        v-model="newAttendeeForm.lastName"
        prefix-icon="el-icon-user"
      />
    </el-form-item>

    <el-form-item label="Email Address" prop="emailAddress">
      <el-input
        v-model="newAttendeeForm.emailAddress"
        prefix-icon="el-icon-promotion"
        placeholder="john@email.org"
        type="email"
      />
    </el-form-item>

    <el-form-item label="Phone Number" prop="phoneNumber">
      <el-input
        v-model="newAttendeeForm.emailAddress"
        prefix-icon="el-icon-phone-outline"
        placeholder="08012345678"
        type="tel"
      />
    </el-form-item>

    <el-form-item>
      <el-button
        type="primary"
        class="NewAttendeeForm__submit-btn"
        v-loading="loading"
        :disabled="loading"
        @click="handleSubmit"
      >
        Register Attendee
      </el-button>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  name: 'NewAttendeeForm',
  props: {
    loading: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      newAttendeeForm: {
        firstName: '',
        lastName: '',
        phoneNumber: '',
        emailAddress: ''
      },
      newAttendeeFormRules: {
        firstName: [{ required: false, message: 'First name is required', trigger: 'blur' }],
        lastName: [{ required: false, message: 'Last name is required', trigger: 'blur' }],
        phoneNumber: [
          { validator: this.validatePhoneNumber, trigger: 'blur' },
          { required: false }
        ],
        emailAddress: [
          { validator: this.validateEmailAddress, trigger: 'blur' },
          { required: false }
        ]
      }
    };
  },
  methods: {
    handleSubmit() {
      this.$refs.newAttendeeForm.validate((valid) => {
        if (valid) {
          this.$emit('submit', this.newAttendeeForm);
        } else {
          return false;
        }
      });
    },
    validatePhoneNumber(rule, value, callback) {
      if (!(/^0[8|9|7][0|1]\d{8}$/g.test(value))) {
        callback(new Error('Please enter a valid phone number'));
      } else {
        callback();
      }
    },
    validateEmailAddress(rule, value, callback) {
      if (!(/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/g.test(value))) {
        callback(new Error('Please enter a valid email address'));
      } else {
        callback();
      }
    }
  }
}
</script>

<style lang="scss">
.NewAttendeeForm {
  &__submit-btn {
    width: 100%;
  }
}
</style>
