<template>
  <el-table
    style="width: 100%"
    :data="admins"
    v-loading="isLoading"
    class="AdminTable"
  >
    <el-table-column
      prop="name"
      label="Name"
      v-slot="{ row }"
    >
      {{row.name || '-- --'}}
    </el-table-column>
    <el-table-column
      prop="emailAddress"
      label="Email"
    />
    <el-table-column
      prop="isUsingDefaultPassword"
      label="Default Password"
      v-slot="{ row }"
    >
      {{ row.isUsingDefaultPassword ? 'Yes' : 'No' }}
    </el-table-column>
    <el-table-column
      prop="addedAt"
      label="Added At"
      v-slot="{ row }"
    >
      {{ row.addedAt | dateFilter }}
    </el-table-column>
    <el-table-column
      label=""
      width="120">
      <template slot-scope="{ row }">
        <el-button
          type="text"
          size="small"
          @click="handleEditAdminPassword(row)"
          :disabled="!isCurrentUser(row)"
        >
          Edit Password
        </el-button>
        <!-- <el-popconfirm
          title="Are you sure to delete this?"
          class="Events__table-delete-button"
          icon-color="red"
          confirm-button-text="Delete"
          cancel-button-text="No"
          @confirm="handleDeleteAdmin(row)"
        >
          <el-button
            slot="reference"
            type="danger"
            size="mini"
            icon="el-icon-delete"
            circle
          />
        </el-popconfirm> -->
      </template>
    </el-table-column>
  </el-table>
</template>

<script>
export default {
  name: 'AdminTable',
  props: {
    admins: {
      type: Array,
      default: () => ([])
    },
    isLoading: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    handleDeleteAdmin(row) {
      this.$emit('delete-event', row);
    },
    handleEditAdminPassword({ id }) {
      this.$emit('edit-password', id);
    },
    isCurrentUser({ id }) {
      const admin = localStorage.getItem('admin');
      return JSON.parse(admin).id === id;
    }
  }
}
</script>

<style lang="scss">
  .AdminTable {
    &__delete-button {
      margin-left: 7px;
    }
  }
</style>