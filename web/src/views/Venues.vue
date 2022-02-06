<template>
  <div>
    <el-empty
      description="No Venues available" 
      v-if="!isLoadingVenues && venues.length < 1"
      :image-size="250"
    >
      <el-button  type="primary">
        <i class="el-icon-plus" />
        Add Venue
      </el-button>
    </el-empty>
  </div>
</template>

<script>
import api from '@/utils/api';

export default {
  name: 'Venues',
  data() {
    return {
      isLoadingVenues: false,
      venues: []
    };
  },
  methods: {
    async getVenues() {
      this.isLoadingVenues = true;

      try {
        const { data } = await api.get('/v1/venues')

        console.log(data, 'data')
        this.venues = data;
      } catch (err) {
        const { data, status } = err.response;

        console.log(status, data.message);
      } finally {
        this.isLoadingVenues = false;
      }
    }
  },
  mounted() {
    this.getVenues();
  }
}
</script>

<style>

</style>