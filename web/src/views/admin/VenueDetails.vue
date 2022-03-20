<template>
  <div class="VenueDetails">
    <el-page-header
      @back="goBack"
      content="Venue Details"
    />

    <el-divider />

    <el-row class="VenueDetails__venue-detail">
      <el-col :span="20" :offset="2">
        <el-descriptions title="Venue Info" border v-loading="isLoadingVenue">
          <el-descriptions-item label="Name">{{venue.name}}</el-descriptions-item>
          <el-descriptions-item label="Number of Seats">{{venue.numOfSeats}}</el-descriptions-item>
      </el-descriptions>
      </el-col>
    </el-row>

    <el-row>
      <el-col :span="20" :offset="2">
        <el-card shadow="never" >
          <el-table
            :data="paginatedVenue"
            v-loading="isLoadingVenue"
          >
            <el-table-column
              prop="number"
              label="Seat Number"
              />
            <el-table-column
              prop="category"
              label="Category"
            />
            <el-table-column
              prop="associatedNumber"
              label="Associated Number"
            />
            <el-table-column
              label=""
              width="120">
              <template slot-scope="{ row }">
                <el-popconfirm
                  title="Are you sure to delete this?"
                  class="VenueDetails__table-delete-button"
                  icon-color="red"
                  confirm-button-text="Delete"
                  cancel-button-text="No"
                  @confirm="handleDeleteSeat(row)"
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

          <el-pagination
            background
            layout="prev, pager, next"
            :total="venueSeatLength"
            :current-page.sync="paginationObj.page"
            class="VenueDetails__seat-paginator"
          >
          </el-pagination>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import api from '@/utils/api';
import { AlertMixin } from '@/mixins';

export default {
  name: 'VenueDetails',
  mixins: [AlertMixin],
  props: {
    venueId: {
      type: String,
      required: true
    }
  },
  data() {
    return {
      venue: {},
      isLoadingVenue: false,
      paginationObj: {
        page: 1,
        limit: 10
      }
    };
  },
  computed: {
    paginatedVenue() {
      const { page, limit } = this.paginationObj;

      const start = ((page - 1) * limit) + 1;

      return (this.venue.seats || []).slice(start, start + limit);
    },
    venueSeatLength() {
      return (this.venue.seats || []).length;
    }
  },
  methods: {
    goBack() {
      this.$router.push({ path: '/admin/venues' });
    },
    async getVenueDetails() {
      this.isLoadingVenue = true;

      try {
        const { data } = await api.get(`/v1/venues/${this.venueId}`);

        this.venue = data;
      } catch (error) {
        this.handleError(error)
      } finally {
        this.isLoadingVenue = false;
      }
    },
    async deleteSeat(number) {
      try {
        await api.delete(`/v1/venues/${this.venue.id}/seats/${number}`);

        this.getVenueDetails()
      } catch (error) {
        this.handleError(error)
      }
    },
    handleDeleteSeat({ number }) {
      this.deleteSeat(number);
    }
  },
  mounted() {
    this.getVenueDetails();
  }
}
</script>

<style lang="scss">
  .VenueDetails {
    &__venue-detail {
      margin-bottom: 30px;
    }

    &__seat-paginator {
      margin-top: 20px;
    }
  }
</style>