<template>
  <div class="Events">
    <el-empty
        description="No Events available"
        v-if="!isLoadingEvents && events.length < 1"
        :image-size="250"
    >
      <el-button type="primary" @click="showEventForm = true">
        <i class="el-icon-plus"/>
        Add Event
      </el-button>
    </el-empty>

    <el-row class="Events__header-row" v-if="events.length > 0">
      <el-button type="primary" @click="showEventForm = true" size="small">
        <i class="el-icon-plus"/>
        Add Event
      </el-button>
    </el-row>

    <el-card shadow="never" v-if="events.length > 0">
      <event-table
          :events="events"
          :is-loading="isLoadingEvents || isDeleteingEvent || isCreatingEvent"
          @delete-event="handleDeleteEvent"
      />
    </el-card>

    <event-dialog
        :show-event-form="showEventForm"
        :venues="venues"
        :is-creating-event="isCreatingEvent"
        @create-event="handleAddEvent"
        @close="showEventForm = false"
    />
  </div>
</template>

<script>
import api from '@/utils/api';
import { AlertMixin } from '@/mixins';
import EventDialog from '@/components/EventDialog.vue';
import EventTable from '@/components/EventTable.vue';

export default {
  name: 'Events',
  mixins: [AlertMixin],
  components: {
    EventDialog,
    EventTable
  },
  data() {
    return {
      isLoadingEvents: false,
      events: [],
      venues: [],
      showEventForm: false,
      isDeleteingEvent: false,
      isCreatingEvent: false
    };
  },
  methods: {
    async getEvents() {
      this.isLoadingEvents = true;

      try {
        const { data } = await api.get('/v1/events');
        this.events = data;
      } catch (error) {
        this.handleError(error);
      } finally {
        this.isLoadingEvents = false;
      }
    },
    async getVenues() {
      this.isLoadingVenues = true;

      try {
        const { data } = await api.get('/v1/venues');

        this.venues = data;
      } catch (error) {
        this.handleError(error)
      } finally {
        this.isLoadingVenues = false;
      }
    },
    async createEvent(body) {
      this.isCreatingEvents = true;

      try {
        await api.post('/v1/events', body);

        this.getEvents();
        this.showEventForm = false;
      } catch (error) {
        this.handleError(error)
      } finally {
        this.isCreatingEvents = false;
      }
    },
    async deleteEvent(id) {
      this.isDeleteingEvent = true;

      try {
        await api.delete(`/v1/events/${id}`);

        this.getEvents();
        this.showEventForm = false;
      } catch (error) {
        this.handleError(error)
      } finally {
        this.isDeleteingEvent = false;
      }
    },
    handleAddEvent(data) {
      this.createEvent(data);
    },
    handleDeleteEvent({ id }) {
      this.deleteEvent(id);
    }
  },
  mounted() {
    this.getEvents();
    this.getVenues();
  }
};
</script>

<style lang="scss">
.Events {
  &__header-row {
    margin-bottom: 20px;
  }

  &__table-delete-button {
    margin-left: 7px;
  }
}
</style>
