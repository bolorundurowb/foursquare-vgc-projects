<template>
  <el-dialog
    title="Add new Event"
    width="30%"
    class="EventDialog"
    :visible="showEventForm"
    :lock-scroll="false"
    :center="true"
    :destroy-on-close="true"
    @close="handleClose"
  >
    <div class="EventDialog__content">
      <el-form
        :model="eventForm"
        ref="eventForm"
        label-position="top"
        :rules="eventFormRules"
      >
        <el-form-item label="Name" prop="name">
          <el-input v-model="eventForm.name" />
        </el-form-item>

        <el-form-item label="Date" prop="date">
          <el-date-picker
            v-model="eventForm.date" 
            type="datetime"
            placeholder="Select date and time"
          />
        </el-form-item>

        <el-row
          v-for="(venue, index) in eventForm.venues"
          :key="index"
          :gutter="20"
          align="middle"
          type="flex"
        >
          <el-col :span="12">
            <el-form-item
              label="Priority"
              :rules="{ required: true, message: 'Priority is required', trigger: 'blur' }"
              :prop="`venues.${index}.priority`"
            >
              <el-input v-model.number="venue.priority"/>
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item
              label="Venue"
              :rules="{ required: true, message: 'Venue is required', trigger: 'blur' }"
              :prop="`venues.${index}.venueId`"
            >
              <el-select
                v-model="venue.venueId"
                placeholder="Select a priority"
              >
                <el-option
                  v-for="item in venues"
                  :key="item.id"
                  :label="item.name"
                  :value="item.id">
                </el-option>
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="4">
            <el-button
              type="danger"
              icon="el-icon-delete"
              circle
              class="EventDialog__delete-button"
              :disabled="index === 0"
              @click="handleRemoveVenue(index)"
            />
          </el-col>
        </el-row>

        <el-form-item>
          <el-button size="small" plain type="primary" @click="handleAddVenue">
            Add Venue
          </el-button>
        </el-form-item>

        <el-form-item>
          <el-button
            type="primary"
            @click="handleAddEvent"
            :disabled="isCreatingEvent"
            v-loading="isCreatingEvent"
          >
            Add Event
          </el-button>
        </el-form-item>
      </el-form>
    </div>
  </el-dialog>
</template>

<script>
export default {
  name: 'EventDialog',
  props: {
    showEventForm: {
      type: Boolean,
      default: false
    },
    isCreatingEvent: {
      type: Boolean,
      default: false
    },
    venues: {
      type: Array,
      default: () => ([])
    }
  },
  data() {
    return {
      eventForm: {
        name: '',
        date: '',
        venues: [
          {
            priority: '',
            venueId: ''
          }
        ]
      },
      eventFormRules: {
        name: [{ required: true, message: 'Name is required' }],
        date: [{ required: true, message: 'Date is required' }],
        venueId: { required: true, message: 'Venue is required' },
        priority: { required: true, message: 'Priority is required' }
      },
      priorityOptions: [
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
    handleRemoveVenue(index) {
      this.eventForm.venues.splice(index, 1);
    },
    handleAddVenue() {
      this.eventForm.venues.push({
        priority: '',
        venueId: ''
      });
    },
    handleAddEvent() {
      this.$refs.eventForm.validate((valid) => {
        if (valid) {
          this.$emit('create-event', this.eventForm);
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
  .EventDialog {
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