<template>
  <el-form
    :model="personCheckin"
    ref="personCheckin"
    label-position="top"
    :rules="personCheckinRules"
    class="PersonCheckin__form"
  >
    <el-form-item label="Person" prop="personId">
      <el-select
        v-model="personCheckin.personId"
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

    <el-form-item label="Seat Category" prop="category">
      <el-select
        v-model="personCheckin.category"
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

    <el-form-item>
      <el-button
        type="primary"
        class="SeatSelectionForm__submit-btn"
        v-loading="isLoading"
        :disabled="isLoading"
        @click="handleSubmit"
      >
        Checkin
      </el-button>
    </el-form-item>
  </el-form>
</template>

<script>
import api from '@/utils/api';
import debounce from 'lodash.debounce';
import { AlertMixin } from '@/mixins';

export default {
  name: 'PersonCheckin',
  mixins: [AlertMixin],
  props: {
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
      persons: [],
      isLoadingPersons: false,
      personCheckin: {
        category: '',
        personId: ''
      },
      personCheckinRules: {
        personId: [
          { required: true, message: 'Person is required', trigger: 'blur' }
        ],
        category: [
          { required: true, message: 'Phone number is required', trigger: 'blur' }
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
  watch: {
    isLoading(current, previous) {
      if (previous && !current && !this.hasError) {
        this.$refs.personCheckin.resetFields();
      }
    }
  },
  methods: {
    handleSubmit() {
      this.$refs.personCheckin.validate((valid) => {
        if (valid) {
          this.$emit('submit', this.personCheckin);
        } else {
          return false;
        }
      });
    },
    async getPersons(query = '') {
      this.isLoadingPersons = true;
      try {
        const { data } = await api.get(`/v1/persons?name=${query}`);

        this.persons = data;
      } catch (error) {
        this.handleError(error)
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
.PersonCheckin {
  &__submit-btn {
    width: 100%;
  }

  &__description {
    margin-bottom: 20px;
  }
}
</style>