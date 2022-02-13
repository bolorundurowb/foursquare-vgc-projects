<template>
  <div class="SeatSelectionForm">
    <el-descriptions
      title="Your Information"
      direction="vertical"
      border
      :column="2"
      class="SeatSelectionForm__description"
    >
      <el-descriptions-item label="Name">
        {{selectedPerson.fullName}}
      </el-descriptions-item>

      <el-descriptions-item label="Phone">
        {{selectedPerson.phone}}
      </el-descriptions-item>
    </el-descriptions>

    <el-form
      :model="seatSelectionForm"
      ref="seatSelectionForm"
      label-position="top"
      :rules="seatSelectionRules"
      class="SeatSelectionForm__form"
    >

      <el-form-item label="Seat Category" prop="category">
        <el-select
          v-model="seatSelectionForm.category"
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
          v-loading="loading"
          :disabled="loading"
          @click="handleSubmit"
        >
          Checkin
        </el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script>
export default {
  name: 'SeatSelectionForm',
  props: {
    loading: {
      type: Boolean,
      default: false
    },
    selectedPerson: {
      type: Boolean,
      required: true
    }
  },
  data() {
    return {
      seatSelectionForm: {
        category: ''
      },
      seatSelectionRules: {
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
  methods: {
    handleSubmit() {
      this.$refs.seatSelectionForm.validate((valid) => {
        if (valid) {
          this.$emit('submit', this.seatSelectionForm);
        } else {
          return false;
        }
      });
    }
  }
}
</script>

<style lang="scss">
.SeatSelectionForm {
  &__submit-btn {
    width: 100%;
  }

  &__description {
    margin-bottom: 20px;
  }
}
</style>