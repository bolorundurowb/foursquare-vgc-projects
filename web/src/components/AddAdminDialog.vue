<template>
  <el-dialog
    class="AddAdminDialog"
    title="Add New Admin"
    width="30%"
    :visible="showAddAdminForm"
    :lock-scroll="false"
    :center="true"
    :destroy-on-close="true"
    @close="handleClose"
  >
    <div class="AddAdminDialog__content">
      <el-form
        :model="addAdminForm"
        ref="addAdminForm"
        label-position="top"
        :rules="addAdminFormRules"
      >
        <el-form-item label="Name" prop="name">
          <el-input v-model="addAdminForm.name" />
        </el-form-item>

        <el-form-item label="Email" prop="emailAddress">
          <el-input v-model="addAdminForm.emailAddress" type="email" />
        </el-form-item>

        <el-form-item label="Password" prop="password">
          <el-input v-model="addAdminForm.password" type="password" />
        </el-form-item>

        <el-form-item>
          <el-button
            type="primary"
            @click="handleAddAdmin"
            :disabled="isCreatingAdmin"
            v-loading="isCreatingAdmin"
          >
            Add Admin
          </el-button>
        </el-form-item>
      </el-form>
    </div>
  </el-dialog>
</template>

<script>
export default {
  name: 'AddAdminDialog',
  props: {
    showAddAdminForm: {
      type: Boolean,
      default: false
    },
    isCreatingAdmin: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      addAdminForm: {
        name: '',
        password: '',
        emailAddress: ''
      },
      addAdminFormRules: {
        name: [{ required: true, message: 'Name is required' }],
        password: [{ required: true, message: 'Password is required' }],
        emailAddress: [{ required: true, message: 'Email is required' }]
      }
    };
  },
  methods: {
    handleClose() {
      this.$emit('close');
    },
    handleAddAdmin() {
      this.$refs.addAdminForm.validate((valid) => {
        if (valid) {
          this.$emit('create-admin', this.addAdminForm);
        } else {
          return false;
        }
      });
    }
  }
}
</script>

<style>

</style>