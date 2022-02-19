<template>
  <el-form
    :model="changeSeatForm"
    :rules="changeSeatFormRules"
    class="ChangeSeatForm"
    ref="changeSeatForm"
    label-position="top"
  >
    <el-form-item label="Person" prop="personId">
      <el-select
        v-model="changeSeatForm.personId"
        placeholder="Select person"
        filterable
        remote
        clearable
        :remote-method="handlePersonSearch"
        :loading="isLoadingPersons"
        @visible-change="handleVisibleChange"
      >
        <el-option
          v-for="person in persons"
          :key="person.id"
          :label="`${person.lastName} ${person.firstName}`"
          :value="person.id"
        />
      </el-select>
    </el-form-item>

    <el-form-item label="Venue" prop="venueId">
      <el-select
        v-model="changeSeatForm.venueId"
        placeholder="Select venue"
      >
        <el-option
          v-for="venue in availableVenue"
          :key="venue.id"
          :label="venue.name"
          :value="venue.id"
        />
      </el-select>
    </el-form-item>

    <el-form-item label="Seat Number" prop="seatNumber">
      <el-select
        v-model="changeSeatForm.seatNumber"
        placeholder="Select Seat"
        :disabled="!changeSeatForm.venueId"
      >
        <el-option
          v-for="seat in availableSeats"
          :key="seat.number"
          :label="seat.number"
          :value="seat.number"
        >
          <div class="ChangeSeatForm__seat-selection-option">
            <span>{{seat.number}}</span>
            <el-tag size="small">{{seat.category}}</el-tag>
          </div>
        </el-option>
      </el-select>
    </el-form-item>

    <el-form-item>
      <el-button
        type="primary"
        v-loading="isLoading"
        :disabled="isLoading"
        @click="submitForm"
      >
        Change Seat
      </el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import api from '@/utils/api';
import debounce from 'lodash.debounce';
import { AlertMixin } from '@/mixins';

export default {
  name: 'ChangeSeatForm',
  mixins: [AlertMixin],
  props: {
    availableSeatsAndVenues: {
      type: Array,
      required: true
    },
    isLoading: {
      type: Boolean,
      default: false
    },
    hasError: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      venues: [],
      persons: [],
      selectedPerson: null,
      isLoadingPersons: false,
      changeSeatForm: {
        personId: '',
        venueId: '',
        seatNumber: ''
      },
      changeSeatFormRules: {
        personId: [{ required: true, message: 'Person is required' }],
        venueId: [{ required: true, message: 'Venue is required' }],
        seatNumber: [{ required: true, message: 'Seat Number is required' }]
      }
    }
  },
  computed: {
    availableVenue() {
      return this.availableSeatsAndVenues.map(({ id, name }) => ({ id, name }));
    },
    availableSeats() {
      if (this.changeSeatForm.venueId) {
        const { venueId } = this.changeSeatForm;
        const foundVenue = this.availableSeatsAndVenues.find(({ id }) => id === venueId);
  
        return foundVenue.seats;
      }

      return [];
    }
  },
  watch: {
    isLoading(current, previous) {
      if (previous && !current && !this.hasError) {
        this.$refs.changeSeatForm.resetFields();
      }
    }
  },
  methods: {
    submitForm() {
      this.$refs.changeSeatForm.validate((valid) => {
        if (valid) {
          this.$emit('submit', this.changeSeatForm);
        } else {
          return false;
        }
      });
    },
    async getVenues() {
      try {
        const { data } = await api.get('/v1/venues');

        this.venues = data;
      } catch (error) {
        const { data } = error.response;

        this.handleError(data.message);
      }
    },
    async getPersons(query = '') {
      this.isLoadingPersons = true;
      try {
        const { data } = await api.get(`/v1/persons?name=${query}`);

        this.persons = data;
      } catch (error) {
        const { data } = error.response;

        this.handleError(data.message);
      } finally {
        this.isLoadingPersons = false;
      }
    },
    handleVisibleChange(value) {
      if (!value) {
        this.getPersons();
      }
    }
  },
  mounted() {
    this.getPersons();
  },
  beforeMount() {
    this.handlePersonSearch = debounce(
      (query) => this.getPersons(query),
      500
    );
  }
}
</script>

<style lang="scss">
.ChangeSeatForm {
  &__seat-selection-option {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }
}
</style>