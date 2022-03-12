<template>
  <div class="Admins" v-loading="isLoadingAdmins">
    <el-empty
        description="No Admins available"
        v-if="!isLoadingAdmins && admins.length < 1"
        :image-size="250"
    >
      <el-button type="primary" @click="showAddAdminForm = true">
        <i class="el-icon-plus"/>
        Add Admin
      </el-button>
    </el-empty>

    <el-row class="Admins__header-row" v-if="admins.length > 0">
      <el-button type="primary" @click="showAddAdminForm = true" size="small">
        <i class="el-icon-plus"/>
        Add Admin
      </el-button>
    </el-row>

    <el-card shadow="never" v-if="admins.length > 0">
      <admin-table
        :admins="admins"
        :is-loading="isLoadingAdmins"
        @edit-password="handleShowEditPasswordDialog"
      />
    </el-card>

    <admin-edit-password
      :show-edit-admin-password-form="showEditPasswordForm"
      :is-editing-password="isLoadingEditAdminPassword"
      @close="handleCloseAdminEditForm"
      @confirm="handleEditPassword"
    />

    <add-admin-dialog
      :is-creating-admin="isLoadingAddAdmin"
      :show-add-admin-form="showAddAdminForm"
      @close="handleCloseAddAdminForm"
      @create-admin="handleCreateAdmin"
    />
  </div>
</template>

<script>
import api from '@/utils/api';
import { AlertMixin } from '@/mixins';
import AdminTable from '@/components/AdminTable.vue';
import AdminEditPassword from '@/components/EditAdminPasswordDialog.vue';
import AddAdminDialog from '@/components/AddAdminDialog.vue';

export default {
  name: 'Admins',
  mixins: [AlertMixin],
  components: {
    AdminTable,
    AddAdminDialog,
    AdminEditPassword
  },
  data() {
    return {
      isLoadingAdmins: false,
      isLoadingEditAdminPassword: false,
      admins: [],
      showAddAdminForm: false,
      isLoadingAddAdmin: false,
      showEditPasswordForm: false
    };
  },
  methods: {
    async getAdmins() {
      this.isLoadingAdmins = true;

      try {
        const { data } = await api.get('/v1/admins');
        this.admins = data;
      } catch (error) {
        this.handleError(error);
      } finally {
        this.isLoadingAdmins = false;
      }
    },
    async editAdminPassword(body) {
      this.isLoadingEditAdminPassword = true;

      try {
        await api.put('/v1/admins/current/password', body);
        this.showEditPasswordForm = false;
        this.handleSuccess('Password changed successully');
      } catch (error) {
        this.handleError(error);
      } finally {
        this.isLoadingEditAdminPassword = false;
      }
    },
    async addAdmin(body) {
      this.isLoadingAddAdmin = true;

      try {
        await api.post('/v1/admins', body);
        this.getAdmins();
        this.showAddAdminForm = false;
      } catch (error) {
        this.handleError(error);
      } finally {
        this.isLoadingAddAdmin = false;
      }
    },
    handleShowEditPasswordDialog() {
      this.showEditPasswordForm = true;
    },
    handleCloseAdminEditForm() {
      this.showEditPasswordForm = false;
    },
    handleEditPassword(form) {
      this.editAdminPassword(form);
    },
    handleCloseAddAdminForm() {
      this.showAddAdminForm = false;
    },
    handleCreateAdmin(form) {
      this.addAdmin(form);
    }
  },
  mounted() {
    this.getAdmins();
  }
}
</script>

<style lang="scss" scoped>
.Admins {
  &__header-row {
    margin-bottom: 20px;
  }
}
</style>