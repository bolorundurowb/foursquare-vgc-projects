<template>
  <el-table
    class="VenueTable"
    stripe
    style="width: 100%"
    :data="venues"
    v-loading="isLoading"
  >
    <el-table-column
      prop="name"
      label="Name" />
    <el-table-column
      prop="numOfSeats"
      label="Number of seats" />
    <el-table-column
      label=""
      width="120">
      <template slot-scope="{ row }">
        <el-button type="text" size="small">Details</el-button>
        <el-popconfirm
          title="Are you sure to delete this?"
          class="VenueTable__delete-button"
          icon-color="red"
          confirm-button-text="Delete"
          cancel-button-text="No"
          @confirm="handleDeleteVenue(row)"
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
  name: 'VenueTable',
  props: {
    venues: {
      type: Array,
      default: () => ([])
    },
    isLoading: {
      type: Boolean,
      default: false
    }
  },
  methods: {
    handleDeleteVenue(row) {
      this.$emit('delete-venue', row);
    }
  }
}
</script>

<style lang="scss">
  .VenueTable {
    &__delete-button {
      margin-left: 7px;
    }
  }
</style>