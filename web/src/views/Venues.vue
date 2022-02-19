<template>
  <div class="Venues">
    <el-empty
      description="No Venues available" 
      v-if="!isLoadingVenues && venues.length < 1"
      :image-size="250"
    >
      <el-button  type="primary" @click="showVenueForm = true">
        <i class="el-icon-plus" />
        Add Venue
      </el-button>
    </el-empty>

    <el-row class="Venues__header-row" v-if="venues.length > 0">
      <el-button type="primary" @click="showVenueForm = true" size="small">
        <i class="el-icon-plus" />
        Add Venue
      </el-button>
    </el-row>

    <venue-table
      :venues="venues"
      :is-loading="isLoadingVenues || isDeleteingVenue || isCreatingVenue"
      v-if="venues.length > 0"
      @delete-venue="handleDeleteVenue"
    />

    <!-- dialog here -->
    <venue-dialog
      :show-venue-form="showVenueForm"
      :is-creating-venue="isCreatingVenue"
      @create-venue="handleAddVenue"
      @close="showVenueForm = false"
    />
  </div>
</template>

<script>
import api from '@/utils/api';
import { AlertMixin } from '@/mixins';
import VenueTable from '@/components/VenueTable.vue'
import VenueDialog from '@/components/VenueDialog.vue'

export default {
  name: 'Venues',
  mixins: [AlertMixin],
  components: {
    VenueTable,
    VenueDialog
  },
  data() {
    return {
      isLoadingVenues: false,
      isCreatingVenue: false,
      isDeleteingVenue: false,
      venues: [],
      showVenueForm: false
    };
  },
  methods: {
    async getVenues() {
      this.isLoadingVenues = true;

      try {
        const { data } = await api.get('/v1/venues');

        this.venues = data;
      } catch (error) {
        const { data } = error.response;

        this.handleError(data.message);
      } finally {
        this.isLoadingVenues = false;
      }
    },
    async createVenue(body) {
      this.isCreatingVenue = true;

      try {
        await api.post('/v1/venues', body);

        this.getVenues();
        this.showVenueForm = false;
      } catch (error) {
        const { data } = error.response;

        this.handleError(data.message);
      } finally {
        this.isCreatingVenue = false;
      }
    },
    async deleteVenue(id) {
      this.isDeleteingVenue = true;

      try {
        await api.delete(`/v1/venues/${id}`);

        this.getVenues();
        this.showVenueForm = false;
      } catch (error) {
        const { data } = error.response;

        this.handleError(data.message);
      } finally {
        this.isDeleteingVenue = false;
      }
    },
    handleAddVenue(data) {
      this.createVenue(data);
    },
    handleDeleteVenue({ id }) {
      this.deleteVenue(id);
    }
  },
  mounted() {
    this.getVenues();
  }
}
</script>

<style lang="scss">
.Venues {
  &__header-row {
    margin-bottom: 20px;
  }
}
</style>