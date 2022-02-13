<template>
  <el-form
    :model="seatSelectionForm"
    ref="seatSelectionForm"
    label-position="top"
    :rules="seatSelectionRules"
    class="SeatSelectionForm"
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
</template>

<script>
export default {
  name: 'SeatSelectionForm',
  props: {
    loading: {
      type: Boolean,
      default: false
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
}
</style>