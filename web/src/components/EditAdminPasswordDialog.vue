<template>
  <el-dialog
    class="EditAdminPasswordDialog"
    title="Edit Password"
    width="30%"
    :visible="showEditAdminPasswordForm"
    :lock-scroll="false"
    :center="true"
    :destroy-on-close="true"
    @close="handleClose"
  >
    <div class="EditAdminPasswordDialog__content">
      <el-form
        :model="editAdminPasswordForm"
        ref="editAdminPasswordForm"
        label-position="top"
        :rules="editAdminPasswordFormRules"
      >
        <el-form-item label="Password" prop="password">
          <el-input v-model="editAdminPasswordForm.password" type="password" />
        </el-form-item>

        <el-form-item label="Confirm Password" prop="confirmPassword">
          <el-input v-model="editAdminPasswordForm.confirmPassword" type="password" />
        </el-form-item>

        <el-form-item>
          <el-button
            type="primary"
            @click="handleConfirm"
            :disabled="isEditingPassword"
            v-loading="isEditingPassword"
          >
            Confirm
          </el-button>
        </el-form-item>
      </el-form>
    </div>
  </el-dialog>
</template>

<script>
export default {
  name: 'EditAdminPasswordDialog',
  props: {
    showEditAdminPasswordForm: {
      type: Boolean,
      default: false
    },
    isEditingPassword: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      editAdminPasswordForm: {
        password: '',
        confirmPassword: ''
      },
      editAdminPasswordFormRules: {
        password: [{ required: true, message: 'Password is required' }],
        confirmPassword: [
          { required: true, message: 'Confirm Password is required' },
          { validator: this.confirmPasswordValidator }
        ]
      }
    };
  },
  methods: {
    handleClose() {
      this.$emit('close');
    },
    handleConfirm() {
      this.$refs.editAdminPasswordForm.validate((valid) => {
        if (valid) {
          this.$emit('confirm', this.editAdminPasswordForm);
        } else {
          return false;
        }
      });
    },
    confirmPasswordValidator(rule, value, callback) {
      if (value !== this.editAdminPasswordForm.password) {
        callback(new Error('Two passwords don\'t match!'));
      } else {
        callback();
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>