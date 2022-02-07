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

    <el-table
      stripe
      style="width: 100%"
      :data="venues"
      :loading="isLoadingVenues"
      v-if="venues.length > 0"
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
          <template slot-scope="">
            <el-button type="text" size="small">Details</el-button>
            <el-button type="danger" size="mini" icon="el-icon-delete" circle />
          </template>
        </el-table-column>
    </el-table>

    <el-dialog
      title="Add new venue"
      width="30%"
      class="VenuesDialog"
      :visible.sync="showVenueForm"
      :lock-scroll="false"
      :center="true"
      :destroy-on-close="true"
    >
      <div class="VenuesDialog__content">
        <el-form
          :model="venueForm"
          ref="venueForm"
          label-position="top"
        >
          <el-form-item label="Name" prop="name" :rules="venueFormRules.name">
            <el-input v-model="venueForm.name" />
          </el-form-item>



          <el-row
            v-for="(range, index) in venueForm.seatRanges"
            :key="index"
            :gutter="20"
            align="middle"
            type="flex"
          >
            <el-col :span="12">
              <el-form-item label="Seat/Seat Range" :rules="venueFormRules.numberRange" :prop="`numberRage-${index}`">
                <el-input v-model="range.numberRange" placeholder="A12-A100/B12"/>
              </el-form-item>
            </el-col>
            <el-col :span="8">
              <el-form-item label="Category" :rules="venueFormRules.category" :prop="`category-${index}`">
                <el-select
                  v-model="range.category"
                  placeholder="Select a category"
                >
                  <el-option
                    v-for="item in categoryOptions"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value">
                  </el-option>
                </el-select>
              </el-form-item>
            </el-col>
            <el-col :span="4">
              <el-button
                type="danger"
                icon="el-icon-delete"
                circle
                class="VenuesDialog__delete-button"
                :disabled="index === 0"
                @click="handleRemoveSeatCategory(index)"
              />
            </el-col>
          </el-row>

          <el-form-item>
            <el-button size="small" plain type="primary" @click="handleAddSeatCategory">
              Add Seat category
            </el-button>
          </el-form-item>

          <el-form-item>
            <el-button
              type="primary"
              @click="handleAddVenue"
            >
              Add Venue
            </el-button>
          </el-form-item>
        </el-form>
      </div>
    </el-dialog>
  </div>
</template>

<script>
import api from '@/utils/api';

export default {
  name: 'Venues',
  data() {
    return {
      isLoadingVenues: false,
      isCreatingVenues: false,
      venues: [],
      showVenueForm: false,
      venueForm: {
        name: '',
        seatRanges: [
          {
            category: '',
            numberRange: ''
          }
        ]
      },
      venueFormRules: {
        name: [
          { required: true, message: 'Name is required' }
        ],
        numberRange: [
          // { required: true, message: 'Number Range is required' }
        ],
        category: [
          // { required: true, message: 'Category is required' }
        ]
      },
      categoryOptions: [
        {
          value: 'Single',
          label: 'Single'
        },
        {
          value: 'Couples',
          label: 'Couples'
        }
      ]
    };
  },
  methods: {
    async getVenues() {
      this.isLoadingVenues = true;

      try {
        const { data } = await api.get('/v1/venues');

        console.log(data, 'data')
        this.venues = data;
      } catch (error) {
        const { data, status } = error.response;

        console.log(status, data.message);
      } finally {
        this.isLoadingVenues = false;
      }
    },
    async createVenue(body) {
      this.isCreatingVenues = true;

      try {
        await api.post('/v1/venues', body);

        this.getVenues();
        this.showVenueForm = false;
      } catch (error) {
        const { data, status } = error.response;

        console.log(status, data.message);
      } finally {
        this.isCreatingVenues = false;
      }
    },
    handleAddSeatCategory() {
      this.venueForm.seatRanges.push({
        category: '',
        numberRange: ''
      });
    },
    handleRemoveSeatCategory(index) {
      this.venueForm.seatRanges.splice(index, 1);
    },
    handleAddVenue() {
      this.$refs.venueForm.validate((valid) => {
        console.log(this.venueForm, 'venue form')
        if (valid) {
          this.createVenue(this.venueForm);
        } else {
          return false;
        }
      });
    }
  },
  mounted() {
    this.getVenues();
  }
}
</script>

<style lang="scss">
.VenuesDialog {
  &__content {
    max-height: 500px;
    overflow-y: auto;
    overflow-x: hidden;
  }

  &__delete-button {
    margin-top: 22px;
  }
}
</style>