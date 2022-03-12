<template>
  <div class="Admins" v-loading="isLoadingAdmins">
    <el-empty
        description="No Admins available"
        v-if="!isLoadingAdmins && admins.length < 1"
        :image-size="250"
    >
      <el-button type="primary">
        <i class="el-icon-plus"/>
        Add Admin
      </el-button>
    </el-empty>

    <el-row class="Admins__header-row" v-if="admins.length > 0">
      <el-button type="primary" @click="showEventForm = true" size="small">
        <i class="el-icon-plus"/>
        Add Event
      </el-button>
    </el-row>

    <el-card shadow="never" v-if="admins.length > 0">
      <admin-table
        :admins="admins"
        :is-loading="isLoadingAdmins"
      />
    </el-card>
  </div>
</template>

<script>
/* eslint-disable */
import api from '@/utils/api';
import { AlertMixin } from '@/mixins';
import AdminTable from '@/components/AdminTable.vue';

export default {
  name: 'Admins',
  mixins: [AlertMixin],
  components: {
    AdminTable
  },
  data() {
    return {
      isLoadingAdmins: false,
      admins: []
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