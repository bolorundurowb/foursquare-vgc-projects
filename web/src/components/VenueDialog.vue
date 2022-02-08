<template>
  <el-dialog
    title="Add new venue"
    width="30%"
    class="VenuesDialog"
    :visible="showVenueForm"
    :lock-scroll="false"
    :center="true"
    :destroy-on-close="true"
    @close="handleClose"
  >
    <div class="VenuesDialog__content">
      <el-form
        :model="venueForm"
        ref="venueForm"
        label-position="top"
        :rules="venueFormRules"
      >
        <el-form-item label="Name" prop="name">
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
            <el-form-item
              label="Seat/Seat Range"
              :rules="{ required: true, message: 'Seat Range is required', trigger: 'blur' }"
              :prop="`seatRanges.${index}.numberRange`"
            >
              <el-input v-model="range.numberRange" placeholder="A12-A100/B12"/>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item
              label="Category"
              :rules="{ required: true, message: 'Category is required', trigger: 'blur' }"
              :prop="`seatRanges.${index}.category`"
            >
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
            :disabled="isCreatingVenue"
            v-loading="isCreatingVenue"
          >
            Add Venue
          </el-button>
        </el-form-item>
      </el-form>
    </div>
  </el-dialog>
</template>

<script>
export default {
  name: 'VenueDialog',
  props: {
    showVenueForm: {
      type: Boolean,
      default: false
    },
    isCreatingVenue: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
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
        name: [{ required: true, message: 'Name is required' }],
        numberRange: { required: true, message: 'Seat Range is required' },
        category: { required: true, message: 'Category is required' }
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
    }
  },
  methods: {
    handleRemoveSeatCategory(index) {
      this.venueForm.seatRanges.splice(index, 1);
    },
    handleAddSeatCategory() {
      this.venueForm.seatRanges.push({
        category: '',
        numberRange: ''
      });
    },
    handleAddVenue() {
      this.$refs.venueForm.validate((valid) => {
        if (valid) {
          this.$emit('create-venue', this.venueForm);
        } else {
          return false;
        }
      });
    },
    handleClose() {
      this.$emit('close');
    }
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