<template>
  <el-table
    style="width: 100%"
    :data="events"
    v-loading="isLoading"
    class="EventTable"
  >
    <el-table-column
      prop="name"
      label="Name" />
    <el-table-column
      prop="startsAt"
      label="Date"
      v-slot="{ row }"
    >
      {{ row.startsAt | dateFilter }}
    </el-table-column>
    <el-table-column
      prop="numOfAttendees"
      label="Number of Attendees" />
    <el-table-column
      label=""
      width="120">
      <template slot-scope="{ row }">
        <el-button
          type="text"
          size="small"
          @click="handleShowEventDetails(row)"
        >
          Details
        </el-button>
        <el-popconfirm
          title="Are you sure to delete this?"
          class="Events__table-delete-button"
          icon-color="red"
          confirm-button-text="Delete"
          cancel-button-text="No"
          @confirm="handleDeleteEvent(row)"
        >
          <el-button
            slot="reference"
            type="danger"
            size="mini"
            icon="el-icon-delete"
            circle
          />
        </el-popconfirm>
      </template>
    </el-table-column>
  </el-table>
</template>

<script>
export default {
  name: 'EventTable',
  props: {
    events: {
      type: Array,
      default: () => ([])
    },
    isLoading: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    handleDeleteEvent(row) {
      this.$emit('delete-event', row);
    },
    handleShowEventDetails(row) {
      this.$router.push({ path: `/admin/events/${row.id}` });
    }
  }
}
</script>

<style lang="scss">
  .EventTable {
    &__delete-button {
      margin-left: 7px;
    }
  }
</style>